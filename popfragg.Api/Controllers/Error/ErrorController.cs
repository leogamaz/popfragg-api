using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace popfragg.Controllers
{
    [ApiController]
    [Route("error")]
    public class ErrorController : ControllerBase
    {
        private readonly ILogger<ErrorController> _logger;

        public ErrorController(ILogger<ErrorController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult HandleError()
        {
            var exceptionFeature = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var error = exceptionFeature?.Error;

            if (error == null)
                return Problem(title: "Erro desconhecido", statusCode: 500);

            // Tratamento de erros expecificos
            if (error is UnauthorizedAccessException)
                return Problem(title: "Acesso não autorizado", statusCode: 403);

            if (error is ArgumentException)
                return Problem(title: "Parâmetro inválido", detail: error.Message, statusCode: 400);

            return Problem(
                title: "Erro interno no servidor",
                detail: error.Message,
                statusCode: 500
            );
        }
    }
}
