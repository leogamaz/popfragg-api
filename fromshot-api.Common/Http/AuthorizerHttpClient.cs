using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;



namespace fromshot_api.Common.Http
{
    public class AuthorizerHttpClient
    {
        public HttpClient Client { get; }

        public AuthorizerHttpClient(IHttpClientFactory factory)
        {
            Client = factory.CreateClient(HttpClientNames.Authorizer);
        }
    }
}
