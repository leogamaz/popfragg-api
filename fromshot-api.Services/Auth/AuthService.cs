using fromshot_api.Domain.Interfaces.Common.Helpers;
using fromshot_api.Domain.Interfaces.Repository;
using fromshot_api.Domain.Interfaces.Service;
using fromshot_api.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace fromshot_api.Services.Auth
{
    public class AuthService(IOpenIdBuildParamsHelper openIdParamsHelper, ISteamRepository steamRepository) : IAuthService
    {
        private readonly IOpenIdBuildParamsHelper _openIdParamsHelper = openIdParamsHelper;
        private readonly ISteamRepository _steamRepository = steamRepository;

        public async Task<string> AuthSteam(OpenIdAuthModel steamParams)
        {
            //verificar null dos parametros claim_id e identity
            if (!_openIdParamsHelper.CheckSignaturesOpenId(steamParams))
            {
                throw new UnauthorizedAccessException("Acesso Negado");
            }

            //buildar os parametros da requisição steamopenid
            FormUrlEncodedContent content =  _openIdParamsHelper.BuildParamsSteamAuthentication(steamParams);
            
            //chamar o SteamRepository para fazer a requisição
            HttpResponseMessage response = await _steamRepository.AuthUser(content);

            //verificar se a resposta esta valida
            var responseContent = await response.Content.ReadAsStringAsync();
            if (!responseContent.Contains("is_valid:true"))
            {
                throw new UnauthorizedAccessException("Acesso Negado");
            }

            //obter o steam id
            string steamId = steamParams.ClaimedId.Replace("https://steamcommunity.com/openid/id/", "");

            _ = steamId ?? throw new UnauthorizedAccessException("Acesso Negado");

            //TO DO Adicionar nonce do open id no REDIS

            return steamId;
        }
    }
}
