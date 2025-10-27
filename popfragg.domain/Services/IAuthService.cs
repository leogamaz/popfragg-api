using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using popfragg.Domain.Models.Users;
using popfragg.Shared.DTOS.Auth;
using popfragg.Shared.DTOS.Steam;

namespace popfragg.Domain.Services
{
    public interface IAuthService
    {
        Task<User> SignUpAsync(SignUpRequestDTO dto);
        Task<User> SignInAsync(SignInRequestDTO dto);
        Task<User?> SignInWithSteam(SteamAuthResponseDTO dto);
        Task<SignUpResponseDTO> ConvertToSignUpResponseDto(User user);
    }
}
