using popfragg.Infrastructure.Configurations;
using popfragg.Infrastructure.Mappers;
using Microsoft.OpenApi.Models;
using Serilog;
using popfragg.Middlewares;
using popfragg.Middlewares.Staging;
using popfragg.Common.Http;
using popfragg.Configurations.Serilog;
using popfragg.Configurations;
using popfragg.Domain.Helpers;
using Dapper;
using dotenv.net;

var builder = WebApplication.CreateBuilder(args);

DotEnv.Load(options: new DotEnvOptions(probeForEnv: true));
SerilogConfiguration.ConfigureSerilog(builder.Configuration);
builder.Host.UseSerilog();

var connectionStrings = new EnvironmentConfig(builder.Configuration, builder.Environment.EnvironmentName);

builder.Services.ConfigureServices();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureHttpClients();
builder.Services.AddAuthorizerService(connectionStrings);
builder.Services.ConfigureMiddleware();
builder.Services.ConfigureSwagger();
builder.Services.ConfigureCors();

//Mapeador para dapper
DapperMappingHelper.RegisterColumnMappings(typeof(popfragg.Domain.Entities.UserEntitie).Assembly);
SqlMapper.AddTypeHandler(new AppUserDataHandler());


var app = builder.Build();


//Adiciona HTTPS
if( app.Environment.IsStaging() || app.Environment.IsProduction())
{
    app.UseHttpsRedirection();
}

// Configuração do pipeline de requisições
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerWithUI();
}

// Adiciona CORS
app.UseCors("AllowSpecificOrigin");

// Middleware de tratamento de erros
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseStatusCodePages();

// Adiciona autenticação e autorização
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.UseStagingAuth(app.Environment);

app.Run();

public partial class Program { }