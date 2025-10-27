using AutoMapper;
using popfragg.Domain.Repositories;
using popfragg.Infrastructure.Exceptions;
using popfragg.Shared.DTOS.Auth;
using popfragg.Shared.Exceptions;
using popfragg.Shared.Helpers;
using System.Data;
using popfragg.Shared.DTOS.Steam;
using popfragg.Domain.Helpers;
using popfragg.Domain.Constants;
using popfragg.Domain.Models.Users;
using popfragg.Domain.Services;

namespace popfragg.Services
{
    public class AuthService(IAuthRepository _authRepository, IMapper _mapper, IOpenIdBuildParams _openIdBuilder, ISteamOpenIdApi _steamOpenIdApi): IAuthService
    {

        public async Task<User> SignUpAsync(SignUpRequestDTO dto)
        {
            var conflicts = await _authRepository.GetConflictsNewUser(dto);
            Guard.AgainstConflicts(conflicts, new Dictionary<string, (string, string)>
            {
                ["SteamIdConflict"] = ("Steam ID já está em uso", ErrorCodes.SteamIdAlreadyInUse),
                ["NicknameConflict"] = ("Nickname já está em uso", ErrorCodes.NicknameAlreadyInUse),
                ["EmailConflict"] = ("Email já está em uso", ErrorCodes.EmailAlreadyInUse),
            });
            Guard.AgainstTrue(dto.Password != dto.ConfirmPassword, "A senhas não coincidem", code: ErrorCodes.ValidationError);
            var user = _mapper.Map<User>(dto);
            user.ValidateNewUser();
            user = await _authRepository.AddAsync(user);
            return user;
        }
        public async Task<User> SignInAsync(SignInRequestDTO dto)
        {
            var user = await _authRepository.GetByEmailAsync(dto.Email) ?? throw new UnauthorizedAccessException("Email ou senha incorretos");
            Guard.AgainstFalse(user.Password == dto.Password, "Email ou senha incorretos");
            return user;
        }
        public async Task<User?>SignInWithSteam(SteamAuthResponseDTO dto)
        {
            //verificar null dos parametros claim_id e identity
            if (!_openIdBuilder.CheckSignaturesOpenId(dto) && dto == null)
            {
                throw new UnauthorizedAccessException("Falha na autenticação com Steam.");
            }
            //buildar os parametros da requisição steamopenid
            FormUrlEncodedContent content = _openIdBuilder.BuildParamsSteamAuthentication(dto);

            //chamar o api steam para fazer a requisição
            HttpResponseMessage response = await _steamOpenIdApi.AuthUser(content);
            
            //verificar se a resposta esta valida
            var responseContent = await response.Content.ReadAsStringAsync();
            if (!responseContent.Contains("is_valid:true"))
            {
                throw new UnauthorizedAccessException("Acesso Negado");
            }
            //obter o steam id
            _ = dto.ClaimedId ?? throw new UnauthorizedAccessException("Acesso Negado");

            string steamId = dto.ClaimedId.Replace("https://steamcommunity.com/openid/id/", "");

            _ = steamId ?? throw new UnauthorizedAccessException("Acesso Negado");

            var user = await _authRepository.GetBySteamIdAsync(steamId);

            if (user == null) return null;

            return user;
        }

        public async Task<SignUpResponseDTO> ConvertToSignUpResponseDto(User user)
        {
            try
            {
                var _dto = _mapper.Map<SignUpResponseDTO>(user);
                return _dto;
            }
            catch
            {
                throw new DataException("Erro ao converter User para SignUpResponseDTO CONTATE UM DESENVOLVEDOR");
            }
        }
    }
    
}
