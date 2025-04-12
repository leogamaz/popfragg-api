using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using fromshot_api.Common.Exceptions;

namespace fromshot_api.Middlewares
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
                var statusCode = ex switch
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

                // Loga o erro com traceId
                _logger.LogError(ex, "Erro tratado capturado no middleware. TraceId: {TraceId}", traceId);

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
                    Instance = context.Request.Path,
                    Extensions = { ["traceId"] = traceId },
                    
                };
                response.Extensions["message"] = message;

                if (ex is IHasErrorCode withCode)
                {
                    response.Extensions["code"] = withCode.Code;
                }

                // Só mostra o erro real no ambiente de desenvolvimento
                if (_env.IsDevelopment() || _env.IsStaging())
                {
                    response.Detail = ex.ToString();
                }

                await context.Response.WriteAsync(JsonSerializer.Serialize(response));
            }
        }
    }
}
