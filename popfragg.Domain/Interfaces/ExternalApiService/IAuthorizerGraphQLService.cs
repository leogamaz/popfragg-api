using fromshot_api.Domain.DTOS.Authorizer.Requests;
using fromshot_api.Domain.DTOS.Authorizer.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace fromshot_api.Domain.Interfaces.ExternalApiService
{
    public interface IAuthorizerGraphQLService
    {
        public Task<SignUpResponse> SignUp(SignUpRequest request);
    }
}
