using Microsoft.Extensions.Configuration;
using System;
using System.Security.Cryptography;

namespace popfragg.Infrastructure.Configurations
{
    public class EnvironmentConfig
    {
        public readonly string Authorizer;
        public readonly string AuthorizerPublic;
        public readonly string AuthorizerUrl;
        public readonly string AuthorizerJWKS;
        public readonly string WriteDataBase;
        public readonly string ReadOnlyDatabase;
        public readonly string? StagingAuthToken;
        public readonly string EnvironmentName;
        public readonly string JWTPrivateKey;
        public readonly string AuthorizerClientId;

        public EnvironmentConfig(IConfiguration configuration, string environmentName)
        {
            EnvironmentName = environmentName;

            Authorizer = configuration.GetConnectionString("Authorizer") ?? throw new InvalidOperationException("ConnectionStrings not configured.");
            AuthorizerPublic = configuration.GetConnectionString("AuthorizerPublic") ?? throw new InvalidOperationException("ConnectionStrings not configured.");
            AuthorizerUrl = configuration.GetConnectionString("AuthorizerUrl") ?? throw new InvalidOperationException("ConnectionStrings not configured.");
            AuthorizerJWKS = $@"{AuthorizerUrl}/.well-known/jwks.json";
            WriteDataBase = configuration.GetConnectionString("WriteDataBase") ?? throw new InvalidOperationException("ConnectionStrings not configured.");
            ReadOnlyDatabase = configuration.GetConnectionString("ReadOnlyDatabase") ?? throw new InvalidOperationException("ConnectionStrings not configured.");
            AuthorizerClientId = configuration.GetConnectionString("AuthorizerClientId") ?? throw new InvalidOperationException("AuthorizerClientId not configured");
            JWTPrivateKey = Environment.GetEnvironmentVariable("JWT_PRIVATE_KEY_PEM")?.Trim()?.Trim('"') ?? throw new InvalidOperationException("JWT Private not configured");
            using var rsa = RSA.Create();
            rsa.ImportFromPem(JWTPrivateKey.ToCharArray());

            //Configurações por ambiente
            if (IsStaging())
            {
                StagingAuthToken = configuration["STAGING_AUTH_TOKEN"] ?? throw new InvalidOperationException("STAGING_AUTH_TOKEN not configured.");
            }
        }
        public bool IsDevelopment() => EnvironmentName.Equals("Development", StringComparison.OrdinalIgnoreCase);
        public bool IsProduction() => EnvironmentName.Equals("Production", StringComparison.OrdinalIgnoreCase);
        public bool IsStaging() => EnvironmentName.Equals("Staging", StringComparison.OrdinalIgnoreCase);
    }
}