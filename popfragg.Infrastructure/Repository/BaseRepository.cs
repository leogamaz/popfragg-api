using popfragg.Infrastructure.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace popfragg.Infrastructure.Repository
{
    public class BaseRepository
    {
        protected readonly string WriteDb;
        protected readonly string ReadOnlyDb;
        protected readonly string AuthorizerJWKS;
        protected readonly string AuthorizerUrl;
        protected readonly string AuthorizerPublic;
        protected readonly string Authorizer;
        protected readonly string JWTPrivateKey;
        protected readonly string AuthorizerClientId;

        protected BaseRepository(EnvironmentConfig config)
        {
            WriteDb = config.WriteDataBase;
            ReadOnlyDb = config.ReadOnlyDatabase;
            AuthorizerJWKS = config.AuthorizerJWKS;
            AuthorizerUrl = config.AuthorizerUrl;
            AuthorizerPublic = config.AuthorizerPublic;
            Authorizer = config.Authorizer;
            JWTPrivateKey = config.JWTPrivateKey;
            AuthorizerClientId = config.AuthorizerClientId;


        }
    }
}
