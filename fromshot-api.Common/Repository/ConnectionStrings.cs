using Microsoft.Extensions.Configuration;

namespace fromshot_api.Common.Repository
{
    public class ConnectionStrings
    {
        public readonly string Authorizer; // Alterado para public
        public readonly string AuthorizerPublic; // Alterado para public
        // Removido ConnectionString porque não está sendo inicializado no construtor.

        public ConnectionStrings(IConfiguration configuration)
        {
            Authorizer = configuration.GetConnectionString("Authorizer");
            AuthorizerPublic = configuration.GetConnectionString("AuthorizerPublic");
        }
    }
}