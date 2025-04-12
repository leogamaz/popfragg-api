using fromshot_api.Repositories.User;
using fromshot_api.Domain.Interfaces.Service;
using fromshot_api.Services.Auth;
using fromshot_api.Domain.Interfaces.Repository;
using fromshot_api.Repositories.Auth;
using fromshot_api.Domain.Interfaces.Common.Helpers;
using fromshot_api.Common.Helpers;
using Npgsql;
using fromshot_api.Common.Repository.DataBaseConnection;
using fromshot_api.Domain.Interfaces.Common.DataBaseConnection;
using fromshot_api.Common.Configurations;
using fromshot_api.Common.Http;
using fromshot_api.Domain.Interfaces.ExternalApiService;
using fromshot_api.Services.ExternalApiService.Authorizer;

namespace fromshot_api.Middlewares
{
    public static class Services
    {
        public static void ConfigureServices(this IServiceCollection services)
        {
            // Adicionar serviços à injeção de dependência
            services.AddSingleton<EnvironmentConfig>(sp =>
            {
                var config = sp.GetRequiredService<IConfiguration>();
                var env = sp.GetRequiredService<IWebHostEnvironment>();
                return new EnvironmentConfig(config, env.EnvironmentName);
            });

            services.AddScoped<IUserRepository, UserRepository>();
            //services.AddScoped<IUserService,UserService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<ISteamRepository, SteamRepository>();
            services.AddScoped<IOpenIdBuildParams, OpenIdBuildParams>();
            services.AddScoped<IReadOnlyDbConnection, ReadOnlyDbConnection>();
            services.AddScoped<IWriteDbConnection, WriteDbConnection>();
            services.AddScoped<AuthorizerHttpClient>();
            services.AddScoped<SteamHttpClient>();
            services.AddScoped<IAuthorizerGraphQLService, AuthoraizerAuthService>();
        }
    }
}
