using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using fromshot_api.Repositories;
using fromshot_api.Services;
using fromshot_api.Services.User;
using fromshot_api.Repositories.User;
using fromshot_api.Domain.Interfaces.Service;
using fromshot_api.Services.Auth;
using fromshot_api.Domain.Interfaces.Repository;
using fromshot_api.Repositories.Auth;
using fromshot_api.Domain.Interfaces.Common.Helpers;
using fromshot_api.Common.Helpers;
using Keycloak.Net;
using fromshot_api.Common.Repository;

namespace fromshot_api.Configurations
{
    public static class ServicesConfig
    {
        public static void ConfigureServices(this IServiceCollection services)
        {
            // Adicionar serviços à injeção de dependência
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService,UserService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ISteamRepository, SteamRepository>();
            services.AddScoped<IOpenIdBuildParamsHelper, OpenIdBuildParamsHelper>();
            services.AddSingleton<ConnectionStrings>();
        }
    }
}
