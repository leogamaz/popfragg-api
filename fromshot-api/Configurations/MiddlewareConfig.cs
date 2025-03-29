using Microsoft.AspNetCore.Mvc;

namespace fromshot_api.Configurations
{
    public class MiddlewareConfig
    {
        public static void ConfigureMiddleware( IServiceCollection services)
        {
            //Desabilita  a resposta automatica para model state invalido
            //É necessário verificar o model state em cada controller
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true; // Evita resposta automática para ModelState inválido
            });

            // Adiciona CORS
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder => builder.AllowAnyOrigin()
                                      .AllowAnyMethod()
                                      .AllowAnyHeader());
            });

        }
    }
}
