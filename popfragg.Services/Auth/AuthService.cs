using popfragg.Common.Exceptions;
using popfragg.Common.Helpers;
using popfragg.Common.Helpers.Querys;
using popfragg.Domain.DTOS.Authorizer.Requests;
using popfragg.Domain.DTOS.Steam;
using popfragg.Domain.Interfaces.Common.Helpers;
using popfragg.Domain.Interfaces.ExternalApiService;
using popfragg.Domain.Interfaces.Repository;
using popfragg.Domain.Interfaces.Service;
using popfragg.Common.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using popfragg.Domain.DTOS.Authorizer.Responses.SignUp;
using popfragg.Domain.DTOS.Authorizer.Responses.Login;
using popfragg.Infrastructure.Authorizer;
using popfragg.Infrastructure.Configurations;
using popfragg.Domain.Entities;

namespace popfragg.Services.Auth
{
    public class AuthService(
        IOpenIdBuildParams openIdParamsHelper,
        ISteamRepository steamRepository,
        IAuthRepository authRepository , 
        IAuthorizerGraphQLService authorizerGraphQL,
        IUserRepository userRepository,
        EnvironmentConfig envConfig,
        IJwtTokenService jwtTokenService
        ) : IAuthService
    {
        private readonly IOpenIdBuildParams _openIdParamsHelper = openIdParamsHelper;
        private readonly ISteamRepository _steamRepository = steamRepository;
        private readonly IAuthRepository _authRepository = authRepository;
        private readonly IAuthorizerGraphQLService _authorizerGraphQL = authorizerGraphQL;
        private readonly IUserRepository _userRepository = userRepository;
        private readonly EnvironmentConfig _envConfig = envConfig;
        private readonly IJwtTokenService _jwtTokenService = jwtTokenService;
        public async Task<UserEntitie> AuthSteam(SteamAuthOpenIdResponse steamParams)
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

            var user = await _userRepository.GetUserBySteamId(steamId);

            var claims = _jwtTokenService.GenerateClaims(user);

            var token = _jwtTokenService.GenerateToken(claims);

            return user;
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

            Guard.AgainstInvalidPassword(request.Password, code:ErrorCodes.BusinessError);
            Guard.AgainstInvalidEmail(request.Email, code: ErrorCodes.BusinessError);
            Guard.AgainstInvalidName(request.GivenName, code: ErrorCodes.BusinessError);
            Guard.AgainstInvalidNickname(request.Nickname, code: ErrorCodes.BusinessError);
            Guard.AgainstTrue(request.Password != request.ConfirmPassword, "A senhas não coincidem", code:ErrorCodes.BusinessError);

            var conflicts = await _authRepository.CheckNewUserAsync(request);
            Guard.AgainstConflicts(conflicts, new Dictionary<string, (string, string)>
            {
                ["SteamIdConflict"] = ("Steam ID já está em uso", ErrorCodes.SteamIdAlreadyInUse),
                ["NicknameConflict"] = ("Nickname já está em uso", ErrorCodes.NicknameAlreadyInUse),
                ["EmailConflict"] = ("Email já está em uso", ErrorCodes.EmailAlreadyInUse),
            });

            request.AddCommonRole();

            SignUpResponse response = await _authorizerGraphQL.SignUp(request);

            return response;
        }

        public async Task<LoginResponse> SignIn(SignInRequest request)
        {
            var response = await _authorizerGraphQL.SignIn(request);
            return response;
        }

        
    }
}
