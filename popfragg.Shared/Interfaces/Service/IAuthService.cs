using popfragg.Domain.DTOS.Authorizer.Requests;
using popfragg.Domain.DTOS.Authorizer.Responses;
using popfragg.Domain.DTOS.Steam;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace popfragg.Domain.Interfaces.Service
{
    public interface IAuthService
    {
        public Task<string> AuthSteam(OpenIdAuth steamParams);
        public Task<SignUpResponse> SignUpWithSteam(SignUpRequest variables);
        public Task<SignUpResponse> SignUp(SignUpRequest request);
    }
}
