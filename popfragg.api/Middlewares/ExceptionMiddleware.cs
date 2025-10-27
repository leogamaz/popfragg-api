using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using popfragg.Infrastructure.Exceptions;
using popfragg.Shared.Exceptions;
using Serilog.Context;
using System.Text.Json;

namespace popfragg.Api.Middlewares
{
    public class ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
    {
        private readonly RequestDelegate _next = next;
        private readonly ILogger<ExceptionMiddleware> _logger = logger;
        private readonly IHostEnvironment _env = env;

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                var traceId = context.TraceIdentifier;
                var path = context.Request.Path;
                var message = ex.Message;
                int statusCode;
                string codeMessage;

                // Se for PostgresException, pega status, message e codeMessage do handle
                if (ex is DbUpdateException dbEx && dbEx.InnerException is PostgresException pgEx)
                {
                    (statusCode, message, codeMessage) = HandlePostgresException(pgEx);
                }
                else
                {
                    statusCode = ex switch
                    {
                        UnauthorizedAccessException => StatusCodes.Status403Forbidden,
                        ArgumentException => StatusCodes.Status400BadRequest,
                        ValidationException => StatusCodes.Status400BadRequest,
                        BusinessException => StatusCodes.Status422UnprocessableEntity,
                        NotFoundException or KeyNotFoundException => StatusCodes.Status404NotFound,
                        InfrastructureUnavailableException => StatusCodes.Status503ServiceUnavailable,
                        _ => StatusCodes.Status500InternalServerError
                    };

                    codeMessage = (ex as IHasErrorCode)?.Code ?? "Unknown";
                }

                var problem = new ProblemDetails
                {
                    Title = ex switch
                    {
                        ValidationException => "Parâmetro inválido",
                        BusinessException => "Regra de negócio violada",
                        UnauthorizedAccessException => "Acesso não autorizado",
                        NotFoundException or KeyNotFoundException => "Recurso não encontrado",
                        InfrastructureUnavailableException => "Serviço indisponível",
                        _ => "Erro interno no servidor"
                    },
                    Status = statusCode,
                    Instance = path,
                    Extensions =
                    {
                        ["traceId"] = traceId,
                        ["message"] = message,
                        ["code_message"] = codeMessage
                    }
                };

                if (_env.IsDevelopment() || _env.IsStaging())
                    problem.Detail = ex.ToString();

                problem.Extensions["code_message"] = codeMessage;

                // Captura opcional do user_id (vai ser null até você implementar usuários)
                Guid? userId = null;
                if (context.User?.Identity?.IsAuthenticated == true)
                {
                    var claim = context.User.FindFirst("sub"); // ou "id", dependendo do JWT
                    if (claim != null && Guid.TryParse(claim.Value, out var id))
                        userId = id;
                }

                using (LogContext.PushProperty("trace_id", traceId))
                using (LogContext.PushProperty("path", path))
                using (LogContext.PushProperty("message_user", message))
                using (LogContext.PushProperty("code_message", codeMessage))
                using (LogContext.PushProperty("status_code", statusCode))
                using (LogContext.PushProperty("user_id", userId))
                {
                    _logger.LogError(ex,
                        "Erro inesperado. Código: {codeMessage}, TraceId: {TraceId}, Message: {Message}",
                        codeMessage, traceId, message);
                }

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = statusCode;
                await context.Response.WriteAsync(JsonSerializer.Serialize(problem));
            }
        }

        private static (int, string, string) HandlePostgresException(PostgresException ex)
        {
            if (ex.SqlState == "23503") // Violação de FK
            {
                return ex.ConstraintName switch
                {
                    "nadeaccess_userid_fkey" => (400, "Usuário não encontrado.", "FOREIGN_KEY_USER_NOT_FOUND"),
                    "nadeaccess_nadeid_fkey" => (400, "Nade não encontrado.", "FOREIGN_KEY_NADE_NOT_FOUND"),
                    _ => (400, "Violação de integridade referencial.", "FOREIGN_KEY_VIOLATION")
                };
            }

            if (ex.SqlState == "23505") // Unique violation
                return (409, "Registro duplicado.", "UNIQUE_CONSTRAINT_VIOLATION");

            if (ex.SqlState == "23514") // Check violation
                return (400, "Valor inválido: violação de regra de validação.", "CHECK_CONSTRAINT_VIOLATION");

            return (500, "Erro de banco de dados.", "POSTGRES_UNKNOWN_ERROR");
        }

    }


}
