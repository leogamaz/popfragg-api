using popfragg.Common.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace popfragg.Common.Repository
{
    public class BaseRepository
    {
        protected readonly string WriteDb;
        protected readonly string ReadOnlyDb;
        protected readonly string AuthorizerJWKS;
        protected readonly string AuthorizerUrl;
        protected readonly string AuthorizerPublic;
        protected readonly string Authorizer;

        protected BaseRepository(EnvironmentConfig config)
        {
            WriteDb = config.WriteDataBase;
            ReadOnlyDb = config.ReadOnlyDatabase;
            AuthorizerJWKS = config.AuthorizerJWKS;
            AuthorizerUrl = config.AuthorizerUrl;
            AuthorizerPublic = config.AuthorizerPublic;
            Authorizer = config.Authorizer;
            
        }
    }
}
