using popfragg.Domain.Interfaces.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace popfragg.Common.Http
{
    public class SteamHttpClient : ISteamHttpClient
    {
        private readonly SafeHttpClient _safe;

        public SteamHttpClient(IHttpClientFactory factory)
        {
            var client = factory.CreateClient(HttpClientNames.Steam);
            _safe = new SafeHttpClient(client);

        }
        public Task<HttpResponseMessage> PostAsync(string url, HttpContent content, string errorMessage)
        {
            return _safe.PostAsync(url, content, errorMessage);
        }
    }
}
