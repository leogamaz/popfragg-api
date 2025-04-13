using popfragg.Domain.Interfaces.Repository;
using popfragg.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
    

namespace popfragg.Repositories.Auth
{
    public class SteamRepository(IHttpClientFactory httpClientFactory) : ISteamRepository
    {
        private readonly HttpClient _httpClient = httpClientFactory.CreateClient("steamOpenId");

        public async Task<HttpResponseMessage> AuthUser( FormUrlEncodedContent steamParams )
        {

            //Open Id Verifica se a autenticação foi realizada com sucesso
            try
            {
                HttpResponseMessage response = await _httpClient.PostAsync("login", steamParams);


                return response;
            }
            catch ( Exception ex)
            {
                throw new UnauthorizedAccessException("Falha na conexão com a steam: " + ex);

            }
            
        }
    }
}
