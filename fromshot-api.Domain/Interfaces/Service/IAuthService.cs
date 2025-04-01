using fromshot_api.Domain.DTOS.Authorizer;
using fromshot_api.Domain.DTOS.Steam;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fromshot_api.Domain.Interfaces.Service
{
    public interface IAuthService
    {
        public Task<string> AuthSteam(OpenIdAuth steamParams);
        public Task<string> SignUpWithSteam(SignUpSteamRequest variables);
    }
}
