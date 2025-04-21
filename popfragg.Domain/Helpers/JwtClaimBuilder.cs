using popfragg.Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace popfragg.Infrastructure.Authorizer
{
    public class JwtClaimBuilder
    {
        private readonly JwtClaims _claims = new();

        public JwtClaimBuilder WithUserId(string userId)
        {
            _claims.UserId = userId;
            return this;
        }

        public JwtClaimBuilder WithAudience(string audience)
        {
            _claims.Audience = audience;
            return this;
        }

        public JwtClaimBuilder WithIssuer(string issuer)
        {
            _claims.Issuer = issuer;
            return this;
        }

        public JwtClaimBuilder WithRoles(params string[] roles)
        {
            _claims.Roles = roles;
            return this;
        }

        public JwtClaimBuilder WithAllowedRoles(params string[] allowedRoles)
        {
            _claims.AllowedRoles = allowedRoles;
            return this;
        }

        public JwtClaimBuilder WithLoginMethod(string loginMethod)
        {
            _claims.LoginMethod = loginMethod;
            return this;
        }

        public JwtClaimBuilder WithTokenType(string tokenType)
        {
            _claims.TokenType = tokenType;
            return this;
        }

        public JwtClaimBuilder WithIssuedAt(DateTime issuedAt)
        {
            _claims.IssuedAt = issuedAt;
            return this;
        }

        public JwtClaimBuilder WithExpiresAt(DateTime expiresAt)
        {
            _claims.ExpiresAt = expiresAt;
            return this;
        }

        public JwtClaimBuilder WithNonce(string nonce)
        {
            _claims.Nonce = nonce;
            return this;
        }

        public JwtClaims Build()
        {
            return _claims;
        }
    }
}
