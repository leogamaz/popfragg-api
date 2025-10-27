using Microsoft.AspNetCore.Http;
using popfragg.Domain.Models.Users;
using popfragg.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;


namespace popfragg.Services
{
    public class CurrentUserService(IHttpContextAccessor httpContextAccessor) : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        private ClaimsPrincipal? User => _httpContextAccessor.HttpContext?.User;
        public string? Id => User?.FindFirst(JwtRegisteredClaimNames.Sub)?.Value
                          ?? User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        public string? Role => User?.FindFirst(ClaimTypes.Role)?.Value
                             ?? User?.FindFirst("role")?.Value;

        public string? SteamId => User?.FindFirst("steam_id")?.Value;

        public bool IsAuthenticated => User?.Identity?.IsAuthenticated ?? false;


    }
}
