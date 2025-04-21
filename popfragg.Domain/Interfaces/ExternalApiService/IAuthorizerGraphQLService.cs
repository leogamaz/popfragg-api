using popfragg.Domain.DTOS.Authorizer.Requests;
using popfragg.Domain.DTOS.Authorizer.Responses.Login;
using popfragg.Domain.DTOS.Authorizer.Responses.SignUp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace popfragg.Domain.Interfaces.ExternalApiService
{
    public interface IAuthorizerGraphQLService
    {
        public Task<SignUpResponse> SignUp(SignUpRequest request);

        public Task<LoginResponse> SignIn(SignInRequest request);
    }
}
