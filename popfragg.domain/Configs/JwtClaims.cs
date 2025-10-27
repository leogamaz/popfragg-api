using popfragg.Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace popfragg.Domain.Configs
{
    public class JwtClaims
    {
        public required User User { get; set; }
        public required string Audience { get; set; }
        public required string Issuer { get; set; }
        public string LoginMethod { get; set; } = "basic_auth";
        public string TokenType { get; set; } = "access_token";
        public DateTime IssuedAt { get; set; } = DateTime.UtcNow;
        public DateTime ExpiresAt { get; set; } = DateTime.UtcNow.AddMinutes(30);
        public string Nonce { get; set; } = Guid.NewGuid().ToString();

        public IEnumerable<Claim> ToClaims()
        {
            var claims = new List<Claim>
            {
                // 🔹 Padrões JWT / OIDC
                new(JwtRegisteredClaimNames.Sub, User.Id.ToString()),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new(JwtRegisteredClaimNames.Iat, ((DateTimeOffset)IssuedAt).ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64),
                new(JwtRegisteredClaimNames.Exp, ((DateTimeOffset)ExpiresAt).ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64),
                new(JwtRegisteredClaimNames.Nbf, ((DateTimeOffset)IssuedAt).ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64),
                new(JwtRegisteredClaimNames.Aud, Audience),
                new(JwtRegisteredClaimNames.Iss, Issuer),

                // 🔹 Custom claims
                new("steam_id", User.SteamId ?? string.Empty),
                new("login_method", LoginMethod),
                new("nonce", Nonce),
                new("role", User.Role),
                new("token_type", TokenType)
            };

            // 🔹 Scopes (boa prática)
            var scopes = new[] { "openid", "email", "profile" };
            claims.AddRange(scopes.Select(scope => new Claim("scope", scope)));

            return claims;
        }
    }
}
