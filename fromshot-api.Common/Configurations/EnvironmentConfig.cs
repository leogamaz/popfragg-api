using Microsoft.Extensions.Configuration;
using System;

namespace fromshot_api.Common.Configurations
{
    public class EnvironmentConfig
    {
        public readonly string Authorizer; // Alterado para public
        public readonly string AuthorizerPublic; // Alterado para public
        public readonly string AuthorizerUrl;
        public readonly string AuthorizerJWKS;
        public readonly string WriteDataBase;
        public readonly string ReadOnlyDatabase;
        public readonly string StagingAuthToken;


        public EnvironmentConfig(IConfiguration configuration)
        {
            Authorizer = configuration.GetConnectionString("Authorizer") ?? throw new InvalidOperationException("ConnectionStrings not configured.");
            AuthorizerPublic = configuration.GetConnectionString("AuthorizerPublic") ?? throw new InvalidOperationException("ConnectionStrings not configured.");
            AuthorizerUrl = configuration.GetConnectionString("AuthorizerUrl") ?? throw new InvalidOperationException("ConnectionStrings not configured.");
            AuthorizerJWKS = $@"{AuthorizerUrl}/.well-known/jwks.json";
            WriteDataBase = configuration.GetConnectionString("WriteDataBase") ?? throw new InvalidOperationException("ConnectionStrings not configured.");
            ReadOnlyDatabase = configuration.GetConnectionString("ReadOnlyDatabase") ?? throw new InvalidOperationException("ConnectionStrings not configured.");
            StagingAuthToken = configuration["STAGING_AUTH_TOKEN"] ?? throw new InvalidOperationException("STAGING_AUTH_TOKEN not configured.");
        }
    }
}