using fromshot_api.Common.Configurations;
using Microsoft.OpenApi.Models;
using Serilog;
using fromshot_api.Middlewares;
using fromshot_api.Configurations;
using fromshot_api.Middlewares.Staging;




var builder = WebApplication.CreateBuilder(args);

SerilogConfiguration.ConfigureSerilog(builder.Configuration);
builder.Host.UseSerilog();



builder.Services.ConfigureServices();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();


//TODO COLOCAR EM UM ARQUIVO SEPARADO CLIENTES HTTPS PERSONALIZADOS
builder.Services.AddHttpClient("steamOpenId", client =>
{
    client.BaseAddress = new Uri("https://steamcommunity.com/openid/");
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});

builder.Services.AddHttpClient("authorizer", client =>
{
    client.BaseAddress = new Uri("https://authorizer-production-a43d.up.railway.app");
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});

var connectionStrings = new EnvironmentConfig(builder.Configuration);
builder.Services.AddAuthorizerService(connectionStrings);


builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "From Shot",
        Version = "v1",
        Description = "API Cameponatos e-sports",
        Contact = new OpenApiContact
        {
            Name = "Leonardo Gama",
            Email = "leonardogama2000@outlook.com.br",
            Url = new Uri("https://github.com")
        }
    });
});
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder =>
        {
            builder.WithOrigins("http://localhost:4200") // Substitua pela sua origem real
                   .AllowAnyHeader()
                   .AllowAnyMethod()
                    .AllowCredentials(); // Permite o envio de cookies
        });
});

// For�a Kestrel a escutar na porta 80 (padr�o do container)
builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.ListenAnyIP(80); // 
});


var app = builder.Build();

// Configura��o do pipeline de requisi��es
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "FromShot v1");
        c.RoutePrefix = "swagger";
    });
    
}


// Adiciona HTTPS
//app.UseHttpsRedirection();

// Adiciona CORS
app.UseCors("AllowSpecificOrigin");

// Middleware de tratamento de erros
//app.UseExceptionHandler("/error");
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseStatusCodePages();

// Adiciona autentica��o e autoriza��o
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.UseStagingAuth(app.Environment);

app.Run();
