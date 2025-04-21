using Microsoft.IdentityModel.Tokens;
using popfragg.Domain.Entities;
using popfragg.Domain.Helpers;
using popfragg.Domain.Interfaces.Service;
using popfragg.Infrastructure.Configurations;
using popfragg.Infrastructure.Repository;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace popfragg.Infrastructure.Authorizer
{
    public class JwtTokenService(EnvironmentConfig config) : BaseRepository(config), IJwtTokenService
    {
        public string GenerateToken(JwtClaims claimsModel)
        {
            var rsa = RSA.Create();
            rsa.ImportFromPem(JWTPrivateKey.ToCharArray());

            var credentials = new SigningCredentials(new RsaSecurityKey(rsa), SecurityAlgorithms.RsaSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claimsModel.ToClaims()),
                Expires = claimsModel.ExpiresAt,
                SigningCredentials = credentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public JwtClaims GenerateClaims(UserEntitie user)
        {
            if (user == null || user.Id == null || user.Roles == null) 
                throw new UnauthorizedAccessException("Seu usuário contem erros de permissão, contate o suporte.");

            var claims = new JwtClaimBuilder()
                .WithUserId(user.Id)
                .WithAudience(AuthorizerClientId)
                .WithIssuer(AuthorizerUrl)
                .WithRoles(["user", "teste"])
                .WithAllowedRoles([user.Roles])
                .Build();

            return claims;
        }


    }
}