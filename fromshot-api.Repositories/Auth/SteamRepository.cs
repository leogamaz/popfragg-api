using fromshot_api.Domain.Interfaces.Repository;
using fromshot_api.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
    

namespace fromshot_api.Repositories.Auth
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
