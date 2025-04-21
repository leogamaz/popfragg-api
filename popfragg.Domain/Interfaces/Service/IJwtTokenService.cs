using popfragg.Domain.Entities;
using popfragg.Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace popfragg.Domain.Interfaces.Service
{
    public interface IJwtTokenService
    {
        string GenerateToken(JwtClaims claims);
        public JwtClaims GenerateClaims(UserEntitie user);

    }
}
