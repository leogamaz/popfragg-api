using Microsoft.AspNetCore.Mvc;

namespace fromshot_api.Configurations
{
    public class ExceptionMiddleware(RequestDelegate next)
    {
        private readonly RequestDelegate _next = next;

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = ex switch
                {
                    UnauthorizedAccessException => StatusCodes.Status403Forbidden,
                    ArgumentException => StatusCodes.Status400BadRequest,
                    KeyNotFoundException => StatusCodes.Status404NotFound,
                    _ => StatusCodes.Status500InternalServerError
                };

                var response = new ProblemDetails
                {
                    Title = ex is UnauthorizedAccessException ? "Acesso não autorizado" :
                            ex is ArgumentException ? "Parâmetro inválido" :
                            "Erro interno no servidor",
                    Detail = ex.Message,
                    Status = context.Response.StatusCode
                };

                await context.Response.WriteAsJsonAsync(response);
            }
        }
    }

}
