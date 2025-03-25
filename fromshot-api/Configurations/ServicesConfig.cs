using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using fromshot_api.Repositories;
using fromshot_api.Services;
using fromshot_api.Domain.Interfaces;
using fromshot_api.Services.User;
using fromshot_api.Repositories.User;

namespace fromshot_api.Configurations
{
    public static class ServicesConfig
    {
        public static void ConfigureServices(this IServiceCollection services)
        {
            // Adicionar serviços à injeção de dependência
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<UserService>();
        }
    }
}
