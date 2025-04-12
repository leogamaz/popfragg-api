using fromshot_api.Common.Http;
using fromshot_api.Domain.Interfaces.Http;
using System.Net.Http;
using System.Threading.Tasks;

namespace fromshot_api.Common.Http;
public class AuthorizerHttpClient : IAuthorizerHttpClient
{
    private readonly SafeHttpClient _safe;

    public AuthorizerHttpClient(IHttpClientFactory factory)
    {
        var client = factory.CreateClient(HttpClientNames.Authorizer);
        _safe = new SafeHttpClient(client);
    }

    public Task<HttpResponseMessage> PostAsync(string url, HttpContent content, string errorMessage)
    {
        return _safe.PostAsync(url, content, errorMessage);
    }
}
