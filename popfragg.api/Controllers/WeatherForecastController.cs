using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using popfragg.Infrastructure.Environment;
using popfragg.Infrastructure.Exceptions;
using System.ComponentModel.DataAnnotations;

namespace popfragg.Api.Controllers
{
    //TESTES
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController(ILogger<WeatherForecastController> logger, IConfiguration config, IOptions<AppSettings> options) : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger = logger;
        private readonly IConfiguration _config = config;
        private readonly AppSettings _appSettings = options.Value;

        [HttpGet("test-connection")]
        public IActionResult TestConnectionString()
        {
            var connString = _config.GetConnectionString("Default");

            if (string.IsNullOrWhiteSpace(connString))
                return BadRequest("Connection string not found or empty.");

            return Ok(new
            {
                message = "Connection string loaded successfully!",
                value = connString
            });
        }
        [HttpGet("test-connection2")]
        public IActionResult TestConnectionString2()
        {
            var connString = _appSettings.ConnectionStrings.Default;

            if (string.IsNullOrWhiteSpace(connString))
                return BadRequest("Connection string not found or empty.");

            return Ok(new
            {
                message = "Connection string loaded successfully!",
                value = connString
            });
        }

        // Resto do código padrão da controller...
        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            var summaries = new[]
            {
                "Freezing", "Bracing", "Chilly", "Cool", "Mild",
                "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
            };

            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = summaries[Random.Shared.Next(summaries.Length)]
            });
        }

        [HttpGet("throw-test")]
        public IActionResult ThrowTest([FromQuery] string type = "generic")
        {
            // Simula diferentes tipos de exceção
            Exception ex = type.ToLower() switch
            {
                "business" => new BusinessException("Erro de negócio simulado"),
                "validation" => new ValidationException("Validação falhou"),
                "unauthorized" => new UnauthorizedAccessException("Não autorizado"),
                "notfound" => new NotFoundException("Recurso não encontrado"),
                "infra" => new InfrastructureUnavailableException("Serviço externo indisponível"),
                _ => new Exception("Erro genérico para teste")
            };

            throw ex; // vai cair no middleware
        }
    }
}
