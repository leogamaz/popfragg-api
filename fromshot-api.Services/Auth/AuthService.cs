using fromshot_api.Common.Exceptions;
using fromshot_api.Common.Helpers;
using fromshot_api.Common.Helpers.Querys;
using fromshot_api.Common.Http;
using fromshot_api.Domain.DTOS.Authorizer.Requests;
using fromshot_api.Domain.DTOS.Authorizer.Responses;
using fromshot_api.Domain.DTOS.Steam;
using fromshot_api.Domain.Interfaces.Common.Helpers;
using fromshot_api.Domain.Interfaces.ExternalApiService;
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
    public class AuthService(
        IOpenIdBuildParams openIdParamsHelper,
        ISteamRepository steamRepository,
        IAuthRepository authRepository , 
        IAuthorizerGraphQLService authorizerGraphQL
        ) : IAuthService
    {
        private readonly IOpenIdBuildParams _openIdParamsHelper = openIdParamsHelper;
        private readonly ISteamRepository _steamRepository = steamRepository;
        private readonly IAuthRepository _authRepository = authRepository;
        private readonly IAuthorizerGraphQLService _authorizerGraphQL = authorizerGraphQL;

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

        public async Task<SignUpResponse> SignUpWithSteam(SignUpRequest request)
        {

            string? steamId = request.AppData?.SteamId;
            string nickname = request.Nickname;

            await Guard.IfAsync(
                steamId,
                _authRepository.SteamIdExisteAsync,
                id => new BusinessException($"SteamID {id} já foi usado", ErrorCodes.SteamIdAlreadyInUse)
            );

            Guard.AgainstTrue(await _authRepository.NicknameExisteAsync(nickname), "Já existe um usuário com este nic", ErrorCodes.BusinessError);

            var response = await _authorizerGraphQL.SignUp(request);
            return response;

        }

        public async Task<SignUpResponse> SignUp(SignUpRequest request)
        {

            string? steamId = request.AppData?.SteamId;
            string nickname = request.Nickname;

            //Se possui steam id faz validação se já esta associado a uma conta
            await Guard.IfAsync(
                steamId,
                _authRepository.SteamIdExisteAsync,
                id => new BusinessException($"SteamID {id} já foi usado", ErrorCodes.SteamIdAlreadyInUse)
            );

            Guard.AgainstTrue(await _authRepository.NicknameExisteAsync(nickname), "Já existe um usuário com este nick", ErrorCodes.BusinessError);

            var response = await _authorizerGraphQL.SignUp(request);

            return response;
        }
    }
}
