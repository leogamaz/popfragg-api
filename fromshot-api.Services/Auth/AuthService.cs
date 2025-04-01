using fromshot_api.Common.Exceptions;
using fromshot_api.Common.Helpers;
using fromshot_api.Common.Helpers.Querys;
using fromshot_api.Domain.DTOS.Authorizer;
using fromshot_api.Domain.DTOS.Steam;
using fromshot_api.Domain.Interfaces.Common.Helpers;
using fromshot_api.Domain.Interfaces.Repository;
using fromshot_api.Domain.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace fromshot_api.Services.Auth
{
    public class AuthService(IOpenIdBuildParams openIdParamsHelper, ISteamRepository steamRepository, IAuthRepository authRepository ,IHttpClientFactory factory) : IAuthService
    {
        private readonly IOpenIdBuildParams _openIdParamsHelper = openIdParamsHelper;
        private readonly ISteamRepository _steamRepository = steamRepository;
        private readonly IAuthRepository _authRepository = authRepository;
        private readonly HttpClient _client = factory.CreateClient("authorizer");
        public async Task<string> AuthSteam(OpenIdAuth steamParams)
        {
            //verificar null dos parametros claim_id e identity
            if (!_openIdParamsHelper.CheckSignaturesOpenId(steamParams) && steamParams == null)
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
            _ = steamParams.ClaimedId ?? throw new UnauthorizedAccessException("Acesso Negado");
            
            string steamId = steamParams.ClaimedId.Replace("https://steamcommunity.com/openid/id/", "");

            _ = steamId ?? throw new UnauthorizedAccessException("Acesso Negado");

            //TO DO Adicionar nonce do open id no REDIS

            return steamId;
        }

        public async Task<string> SignUpWithSteam(SignUpSteamRequest variables)
        {

            string signUpQuery = GraphQL.GetSignUpQuery();

            string steamId = variables.AppData.SteamId;
            string nickname = variables.Nickname;

            Guard.AgainstTrue(await _authRepository.SteamIdExisteAsync(steamId), "SteamID já foi vinculado a uma conta existente",ErrorCodes.SteamIdAlreadyInUse);

            Guard.AgainstTrue(await _authRepository.NicknameExisteAsync(steamId), "Já existe um usuário com este nic", ErrorCodes.BusinessError);

            var requestBody = new
            {
                query = signUpQuery,
                variables = new
                {
                    data = variables
                }
            };
            var json = Json.SerializeSnakeCase(requestBody);

            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/graphql", content);
            var rawResponse = await response.Content.ReadAsStringAsync();
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync(); 
        }
    }
}
