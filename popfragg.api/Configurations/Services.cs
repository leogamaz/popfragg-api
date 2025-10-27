using popfragg.Domain.Helpers;
using popfragg.Domain.Repositories;
using popfragg.Infrastructure.Repositories.Auth;
using popfragg.Infrastructure.Repositories.NadesRepository;
using popfragg.Services;
using popfragg.Services.External;
using popfragg.Domain.Services;

namespace popfragg.Api.Configurations
{
    public static class Services
    {
        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<INadesRepository, NadesRepository>();
            services.AddScoped<IOpenIdBuildParams, OpenIdBuildParams>();
            services.AddScoped<ISteamOpenIdApi, SteamOpenIdAPI>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<INadesService, NadesService>();
            services.AddScoped<ICurrentUserService, CurrentUserService>();
        }
    }
}
