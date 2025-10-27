using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using popfragg.Domain.Configs;
using popfragg.Domain.Constants;
using popfragg.Domain.Models.Users;
using popfragg.Infrastructure.Environment;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace popfragg.Api.Auth
{
    public class TokenService(IOptions<AppSettings> appSettings)
    {
        private readonly JwtSettings _jwtSettings = appSettings.Value.JwtSettings;

        public string GenerateToken(User user, string? loginMethod = null)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_jwtSettings.Key) ?? throw new InvalidOperationException("JWT Key not configured");
            var jwtClaims = new JwtClaims
            {
                User = user,
                Audience = _jwtSettings.Audience,
                ///CLASSE LoginMethod de constantes criada para identificar o método de login (Steam, Google, etc)
                LoginMethod = loginMethod ?? LoginMethods.Basic,
                Issuer = _jwtSettings.Issuer,
                IssuedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddHours(1)
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(jwtClaims.ToClaims()),
                Expires = jwtClaims.ExpiresAt,
                SigningCredentials = new SigningCredentials
                (
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
