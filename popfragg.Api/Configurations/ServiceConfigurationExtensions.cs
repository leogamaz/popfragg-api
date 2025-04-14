using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using popfragg.Common.Http;
using System.Runtime.CompilerServices;

namespace popfragg.Configurations
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
                        builder.WithOrigins("http://localhost:4200", "https://popfragg.vercel.app") // Substitua pela sua origem real
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

        }

        public static void ConfigureHttpClients(this IServiceCollection services)
        {
            services.AddHttpClient(HttpClientNames.Steam, client =>
            {
                client.BaseAddress = new Uri("https://steamcommunity.com/openid/");
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            });

            services.AddHttpClient(HttpClientNames.Authorizer, client =>
            {
                client.BaseAddress = new Uri("https://authorizer-production-a43d.up.railway.app");
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
    }
}
