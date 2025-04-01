using Microsoft.Extensions.Configuration;
using System;

namespace fromshot_api.Common.Repository
{
    public class ConnectionStrings
    {
        public readonly string Authorizer; // Alterado para public
        public readonly string AuthorizerPublic; // Alterado para public
        public readonly string AuthorizerUrl;
        public readonly string AuthorizerJWKS;


        public ConnectionStrings(IConfiguration configuration)
        {
            Authorizer = configuration.GetConnectionString("Authorizer") ?? throw new InvalidOperationException("ConnectionStrings not configured.");
            AuthorizerPublic = configuration.GetConnectionString("AuthorizerPublic") ?? throw new InvalidOperationException("ConnectionStrings not configured.");
            AuthorizerUrl = configuration.GetConnectionString("AuthorizerUrl") ?? throw new InvalidOperationException("ConnectionStrings not configured.");
            AuthorizerJWKS = $@"{AuthorizerUrl}/.well-known/jwks.json";
        }
    }
}