using fromshot_api.Common.Exceptions;
using fromshot_api.Domain.Interfaces.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace fromshot_api.Common.Http
{
    public class SafeHttpClient(HttpClient client) : ISafeHttpClient
    {
        private readonly HttpClient _client = client;

        public async Task<HttpResponseMessage> PostAsync(string url, HttpContent content, string errorMessage)
        {
            try
            {
                var response = await _client.PostAsync(url, content);
                response.EnsureSuccessStatusCode();
                return response;
            }
            catch (HttpRequestException)
            {
                throw new InfrastructureUnavailableException(errorMessage);
            }
        }

        // TODO : expandir pra GET, PUT, etc
    }
}
