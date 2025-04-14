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



builder.Services.ConfigureServices();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureHttpClients();
var connectionStrings = new EnvironmentConfig(builder.Configuration, builder.Environment.EnvironmentName);
builder.Services.AddAuthorizerService(connectionStrings);
builder.Services.ConfigureMiddleware();
builder.Services.ConfigureSwagger();
builder.Services.ConfigureCors();

// Força Kestrel a escutar na porta 80 (padrão do container)
builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.ListenAnyIP(80); // 
});


var app = builder.Build();

// Configuração do pipeline de requisições
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerWithUI();
    
}

// Adiciona HTTPS
if(app.Environment.IsDevelopment() 
    || app.Environment.IsStaging() 
    || app.Environment.IsProduction())
{
    app.UseHttpsRedirection();
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