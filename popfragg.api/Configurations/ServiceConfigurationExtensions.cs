using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using popfragg.Api.Auth;
using popfragg.Infrastructure.Environment;
using System.Text;

namespace popfragg.Api.Configurations
{
    public static class ServiceConfigurationExtensions
    {
        public static void ConfigureMiddleware(this IServiceCollection services)
        {
            //Desabilita  a resposta automatica para model state invalido
            //É necessário verificar o model state em cada controller

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true; // Evita resposta automática para ModelState inválido
            });


        }

        public static void ConfigureCors(this IServiceCollection services)
        {
            // Adiciona CORS
            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    builder =>
                    {
                        builder.WithOrigins
                            (
                                "http://localhost:4200", "https://localhost:4200", "https://popfragg.vercel.app", "https://stg.popfragg.com",
                                 "http://localhost:5000", "https://localhost:5000"
                            ) 
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                            .AllowCredentials(); // Permite o envio de cookies
                    });
            });
        }

        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "PopFragg API",
                    Version = "v1",
                    Description = "Seu repositório de Counter-Strike 2",
                    Contact = new OpenApiContact
                    {
                        Name = "Leonardo Gama",
                        Email = "leonardogama2000@outlook.com.br",
                        Url = new Uri("https://github.com")
                    }
                });

                // Define o esquema de autenticação JWT corretamente
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,      // <-- era ApiKey, agora Http
                    Scheme = "bearer",                   // <-- precisa ser tudo minúsculo
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Insira o token JWT desta forma: Bearer {seu_token}"
                });

                // Aplica o esquema Bearer em todos os endpoints protegidos
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });
        }

        public static void ConfigureHttpClients(this IServiceCollection services)
        {
            services.AddHttpClient(HttpClientNames.Steam, client =>
            {
                client.BaseAddress = new Uri("https://steamcommunity.com/openid/");
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            });

        }

        public static void UseSwaggerWithUI(this WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "FromShot v1");
                c.RoutePrefix = "swagger";
            });
        }

        public static void ConfigureJwt(this IServiceCollection services, AppSettings appSettings)
        {
            var key = Encoding.UTF8.GetBytes(appSettings.JwtSettings!.Key!);

            // Adiciona autenticação JWT apenas uma vez
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false; // opcional (útil em dev)
                options.SaveToken = true; // opcional: permite recuperar o token atual

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = appSettings.JwtSettings.Issuer,       // "popfragg-api"
                    ValidAudience = appSettings.JwtSettings.Audience,   // "popfragg-frontend"
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = ctx =>
                    {
                        Console.WriteLine($"[JWT ERROR] {ctx.Exception.Message}");
                        return Task.CompletedTask;
                    }
                };
            });

            // Usa AddScoped em vez de Singleton, pois o TokenService pode depender de contexto HTTP no futuro
            services.AddScoped<TokenService>();
        }
    }
}
