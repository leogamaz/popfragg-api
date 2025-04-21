using popfragg.Repositories.User;
using popfragg.Domain.Interfaces.Service;
using popfragg.Services.Auth;
using popfragg.Domain.Interfaces.Repository;
using popfragg.Repositories.Auth;
using popfragg.Domain.Interfaces.Common.Helpers;
using popfragg.Common.Repository.DataBaseConnection;
using popfragg.Domain.Interfaces.Common.DataBaseConnection;
using popfragg.Infrastructure.Configurations;
using popfragg.Common.Http;
using popfragg.Domain.Interfaces.ExternalApiService;
using popfragg.Services.ExternalApiService.Authorizer;
using popfragg.Domain.Interfaces.Http;
using popfragg.Domain.Helpers;
using popfragg.Infrastructure.Http;
using popfragg.Infrastructure.Repository.DataBaseConnection;
using popfragg.Infrastructure.Authorizer;

namespace popfragg.Middlewares
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
            services.AddScoped<ISafeHttpClient,SafeHttpClient>();
            services.AddScoped<IAuthorizerHttpClient, AuthorizerHttpClient>();
            services.AddScoped<ISteamHttpClient, SteamHttpClient>();
            services.AddScoped<IAuthorizerGraphQLService, AuthoraizerAuthService>();
            services.AddScoped<IJwtTokenService , JwtTokenService>();

        }
    }
}
