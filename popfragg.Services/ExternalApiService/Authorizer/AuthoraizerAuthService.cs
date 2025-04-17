using popfragg.Common.Exceptions;
using popfragg.Common.Helpers;
using popfragg.Common.Helpers.Querys;
using popfragg.Common.Http;
using popfragg.Domain.DTOS.Authorizer;
using popfragg.Domain.DTOS.Authorizer.Requests;
using popfragg.Domain.DTOS.Authorizer.Responses.Login;
using popfragg.Domain.DTOS.Authorizer.Responses.SignUp;
using popfragg.Domain.Interfaces.ExternalApiService;
using popfragg.Domain.Interfaces.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace popfragg.Services.ExternalApiService.Authorizer
{
    public class AuthoraizerAuthService(IAuthorizerHttpClient authorizerClient) : IAuthorizerGraphQLService
    {
        private readonly IAuthorizerHttpClient _authorizerClient = authorizerClient;
        private readonly string _graphQLEndpoint = "/graphql";
        private readonly string _contentType = "application/json";

        public async Task<SignUpResponse> SignUp(SignUpRequest request)
        {
            string signUpQuery = GraphQL.GetSignUpQuery();

            var requestBody = new
            {
                query = signUpQuery,
                variables = new
                {
                    data = request
                }
            };

            string json = Json.SerializeSnakeCase(requestBody);

            StringContent content = new(json, Encoding.UTF8, _contentType);

            HttpResponseMessage response = await _authorizerClient.PostAsync(
                _graphQLEndpoint,
                content,
                "Falha no serviço de autenticação"
            );

            string responseBody = await response.Content.ReadAsStringAsync();

            var result = Json.DeserializeSnakeCase<GraphQLResponse<SignUpData>>(responseBody);

            // Trata erro do GraphQL mesmo com status 200
            if (result.Errors != null && result.Errors.Count != 0)
            {
                var errorMessage = result.Errors.First().Message;
                throw new BusinessException($"Erro no signup: {errorMessage}", ErrorCodes.EmailAlreadyInUse);
            }

            if (result.Data == null)
                throw new NotFoundException("Erro desconhecido", ErrorCodes.NotFound);

            return result.Data.Signup;
        }

        public async Task<LoginResponse> SignIn(SignInRequest request)
        {
            string query = GraphQL.GetSignInQuery();

            var requestBody = new
            {
                query,
                variables = request
            };

            string json = Json.SerializeSnakeCase(requestBody);

            StringContent content = new(json, Encoding.UTF8, _contentType);
            var teste = json;
            Console.WriteLine(teste);
            HttpResponseMessage response = await _authorizerClient.PostAsync(
               _graphQLEndpoint,
               content,
               "Falha no serviço de autenticação"
            );

            string responseBody = await response.Content.ReadAsStringAsync();

            var result = Json.DeserializeSnakeCase<GraphQLResponse<LoginData>>(responseBody);

            // Trata erro do GraphQL mesmo com status 200
            if (result.Errors != null && result.Errors.Count != 0)
            {
                var errorMessage = result.Errors.First().Message;
                throw new BusinessException($"Erro no signup: {errorMessage}", ErrorCodes.EmailAlreadyInUse);
            }

            if (result.Data == null)
                throw new NotFoundException("Erro desconhecido", ErrorCodes.NotFound);

            return result.Data.Login;

        }
    }
}
