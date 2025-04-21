using popfragg.Domain.DTOS.Authorizer.Requests;
using popfragg.Domain.DTOS.Authorizer.Responses.Login;
using popfragg.Domain.DTOS.Authorizer.Responses.SignUp;
using popfragg.Domain.DTOS.Steam;
using popfragg.Domain.Entities;
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
        public Task<UserEntitie> AuthSteam(SteamAuthOpenIdResponse steamParams);
        public Task<SignUpResponse> SignUpWithSteam(SignUpRequest variables);
        public Task<SignUpResponse> SignUp(SignUpRequest request);
        public Task<LoginResponse> SignIn(SignInRequest request);
    }
}
