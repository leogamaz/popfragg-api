using System.Diagnostics;
using fromshot_api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace fromshot_api.Controllers
{
    [Authorize] // Exige autenticação para todas as ações deste controller
    [Route("[controller]")] // Define a rota base com o nome do controller (Home)
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;

        public AuthController(ILogger<AuthController> logger)
        {
            _logger = logger;
        }

        // GET: /Home/Index ou apenas /Home se houver rota padrão definida
        [HttpGet("index")]
        public IActionResult Index()
        {
            return Ok("Confirmado");
        }

        // GET: /Home/Privacy
        [HttpGet("privacy")]
        public IActionResult Privacy()
        {
            return Ok();
        }

        // GET: /Home/Error
        [HttpGet("error")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            var errorModel = new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };

            return BadRequest(errorModel);
        }
    }
}