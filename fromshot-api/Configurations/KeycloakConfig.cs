namespace fromshot_api.Configurations
{
    using Keycloak.AuthServices.Authentication;
    using Keycloak.AuthServices.Authorization;
    using Keycloak.AuthServices.Sdk.Admin;

    public static class KeycloakConfig
    {
        public static void AddKeycloakServices(this IServiceCollection services, IConfiguration configuration)
        {
            var keycloakAuthOptions = configuration
                .GetSection(KeycloakAuthenticationOptions.Section)
                .Get<KeycloakAuthenticationOptions>();

            var keycloakAuthorizationOptions = configuration
                .GetSection(KeycloakProtectionClientOptions.Section)
                .Get<KeycloakProtectionClientOptions>();

            var keycloakAdminOptions = configuration
                .GetSection(KeycloakAdminClientOptions.Section)
                .Get<KeycloakAdminClientOptions>();

            if (keycloakAuthOptions == null || keycloakAuthorizationOptions == null || keycloakAdminOptions == null)
            {
                throw new ArgumentNullException(nameof(keycloakAuthOptions), "Configuração do Keycloak não encontrada no appsettings.json");
            }

            services.AddKeycloakAuthentication(keycloakAuthOptions);
            services.AddKeycloakAuthorization(keycloakAuthorizationOptions);
            services.AddKeycloakAdminHttpClient(keycloakAdminOptions);
        }
    }

}
