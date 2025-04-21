using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace popfragg.Domain.Helpers
{
    public class JwtClaims
    {
        public  string UserId { get; set; }
        public  string Audience { get; set; }
        public  string Issuer { get; set; }
        public  string LoginMethod { get; set; } = "basic_auth";
        public  string TokenType { get; set; } = "access_token";
        public  string[] Roles { get; set; }
        public  string[] AllowedRoles { get; set; }
        public  DateTime IssuedAt { get; set; } = DateTime.UtcNow;
        public  DateTime ExpiresAt { get; set; } = DateTime.UtcNow.AddMinutes(30);
        public  string Nonce { get; set; } = Guid.NewGuid().ToString();

        public IEnumerable<Claim> ToClaims()
        {
            var claims = new List<Claim>
            {
                new("sub", UserId),
                new("aud", Audience),
                new("iss", Issuer),
                new("login_method", LoginMethod),
                new("nonce", Nonce),
                new("token_type", TokenType),
                new("iat", ((DateTimeOffset)IssuedAt).ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64),
                new("exp", ((DateTimeOffset)ExpiresAt).ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64),
                new("nbf", ((DateTimeOffset)IssuedAt).ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64)
            };


            claims.AddRange(Roles.Select(role => new Claim("roles", role)));
            claims.AddRange(AllowedRoles.Select(role => new Claim("allowed_roles", role)));

            var scopes = new[] { "openid", "email", "profile" };
            claims.AddRange(scopes.Select(scope => new Claim("scope", scope)));

            return claims;
        }

    }
}
