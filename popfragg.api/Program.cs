using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using popfragg.Api.Auth;
using popfragg.Api.Configurations;
using popfragg.Api.Configurations.Serilog;
using popfragg.Api.Mapping;
using popfragg.Api.Middlewares;
using popfragg.Domain.Repositories;
using popfragg.Infrastructure.Data;
using popfragg.Infrastructure.Environment;
using popfragg.Infrastructure.Repositories;
using popfragg.Services;
using Serilog;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var environment = builder.Environment.EnvironmentName;

// Define qual arquivo .env usar com base no ambiente
var envFileName = environment.ToLower() switch
{
    "development" => ".env.local",
    "staging" => ".env.staging",
    "production" => ".env.production",
    _ => ".env.local" // fallback
};

// Carrega o arquivo .env correspondente, se existir
if (File.Exists(envFileName))
{
    DotNetEnv.Env.Load(envFileName);
    Console.WriteLine($"Variáveis de ambiente carregadas de {envFileName}");
}
else
{
    Console.WriteLine($"Arquivo {envFileName} não encontrado, seguindo sem .env");
}

// Add configuration files
builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
    .AddEnvironmentVariables();

//builder.ConfigureJWT();

// Add services to the container.
builder.Services.Configure<AppSettings>(builder.Configuration);

//Configure DI Services
builder.Services.ConfigureServices();

//Configure JWT Service
var appSettings = builder.Configuration.Get<AppSettings>()!;
builder.Services.ConfigureJwt(appSettings);

//Configure Clients Http
builder.Services.ConfigureHttpClients();

//Configure Serilog
SerilogConfiguration.ConfigureSerilog(builder.Configuration);
builder.Host.UseSerilog();

//Configure EFCore
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(appSettings.ConnectionStrings.Default));

//Add Controllers and Endpoints
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

//Configure Swagger
builder.Services.ConfigureSwagger();

//Configure Mapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

//Configure CORS
builder.Services.ConfigureCors();

var app = builder.Build();

//Adiciona HTTPS
if (app.Environment.IsStaging() || app.Environment.IsProduction())
{
    app.UseHttpsRedirection();
}

// Configuração do pipeline de requisições
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerWithUI();
}

app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseCors("AllowSpecificOrigin");
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
