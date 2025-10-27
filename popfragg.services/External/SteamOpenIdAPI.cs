using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using popfragg.Domain.Helpers;

namespace popfragg.Services.External
{
    public class SteamOpenIdAPI(IHttpClientFactory httpClientFactory) : ISteamOpenIdApi
    {
        private readonly HttpClient _httpClient = httpClientFactory.CreateClient("steamOpenId");

        public async Task<HttpResponseMessage> AuthUser(FormUrlEncodedContent steamParams)
        {

            //Open Id Verifica se a autenticação foi realizada com sucesso
            try
            {
                HttpResponseMessage response = await _httpClient.PostAsync("login", steamParams);


                return response;
            }
            catch (Exception ex)
            {
                throw new UnauthorizedAccessException("Falha na conexão com a steam: " + ex);

            }

        }
    }
}
