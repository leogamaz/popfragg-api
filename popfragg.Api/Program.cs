using popfragg.Common.Configurations;
using Microsoft.OpenApi.Models;
using Serilog;
using popfragg.Middlewares;
using popfragg.Middlewares.Staging;
using popfragg.Common.Http;
using popfragg.Configurations.Serilog;
using popfragg.Configurations;




var builder = WebApplication.CreateBuilder(args);

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

// For�a Kestrel a escutar na porta 80 (padr�o do container)
//builder.WebHost.ConfigureKestrel(serverOptions =>
//{
//    serverOptions.ListenAnyIP(80); // 

//});

var app = builder.Build();


//Adiciona HTTPS
if( app.Environment.IsStaging() || app.Environment.IsProduction())
{
    app.UseHttpsRedirection();
}

// Configura��o do pipeline de requisi��es
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerWithUI();
}

// Adiciona CORS
app.UseCors("AllowSpecificOrigin");

// Middleware de tratamento de erros
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseStatusCodePages();

// Adiciona autentica��o e autoriza��o
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.UseStagingAuth(app.Environment);

app.Run();

public partial class Program { }