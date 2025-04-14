using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using popfragg.Common.Exceptions;
using Microsoft.Extensions.Logging;
using Serilog.Context;
using System.IO;

namespace popfragg.Middlewares
{
    public class ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger, IHostEnvironment env)
    {
        private readonly RequestDelegate _next = next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger = logger;
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
                var message = ex.Message;
                var path = context.Request.Path;
                int statusCode = ex switch
                {
                    UnauthorizedAccessException => StatusCodes.Status403Forbidden,
                    ArgumentException => StatusCodes.Status400BadRequest,
                    var e when e is ValidationException => StatusCodes.Status400BadRequest,
                    var e when e is BusinessException => StatusCodes.Status422UnprocessableEntity,
                    var e when e is NotFoundException => StatusCodes.Status404NotFound,
                    var e when e is InfrastructureUnavailableException => StatusCodes.Status503ServiceUnavailable,
                    KeyNotFoundException => StatusCodes.Status404NotFound,
                    _ => StatusCodes.Status500InternalServerError
                };

                

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = statusCode;
                

                var response = new ProblemDetails
                {
                    Title =
                        ex switch
                        {
                            ValidationException => "Parâmetro inválido",
                            BusinessException => "Regra de negócio violada",
                            UnauthorizedAccessException => "Acesso não autorizado",
                            NotFoundException or KeyNotFoundException => "Recurso não encontrado",
                            InfrastructureUnavailableException => "Serviço indisponivel",
                            _ => "Erro interno no servidor"
                        },
                    Status = statusCode,
                    Instance = path,
                    Extensions = { ["traceId"] = traceId },
                    
                };
                response.Extensions["message"] = message;

                if (ex is IHasErrorCode withCode)
                {
                    response.Extensions["code_message"] = withCode.Code;
                }

                // Só mostra o erro real no ambiente de desenvolvimento
                if (_env.IsDevelopment() || _env.IsStaging())
                {
                    response.Detail = ex.ToString();
                }


                var codeMessage = response.Extensions.TryGetValue("code_message", out var value) ? value?.ToString() : "Unknown";

                using (LogContext.PushProperty("trace_id", traceId))
                using (LogContext.PushProperty("path", path))
                using (LogContext.PushProperty("message_user", message))
                using (LogContext.PushProperty("code_message", codeMessage))
                using (LogContext.PushProperty("status_code",statusCode ))
                {
                    _logger.LogError(ex, "Erro inesperado. Código: {codeMessage}, TraceId: {TraceId}, Message: {message}", codeMessage, traceId,message);
                }

                await context.Response.WriteAsync(JsonSerializer.Serialize(response));
            }
        }
    }
}
