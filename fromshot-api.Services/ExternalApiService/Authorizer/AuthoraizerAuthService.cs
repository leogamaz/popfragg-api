﻿using fromshot_api.Common.Exceptions;
using fromshot_api.Common.Helpers;
using fromshot_api.Common.Helpers.Querys;
using fromshot_api.Common.Http;
using fromshot_api.Domain.DTOS.Authorizer;
using fromshot_api.Domain.DTOS.Authorizer.Requests;
using fromshot_api.Domain.DTOS.Authorizer.Responses;
using fromshot_api.Domain.Interfaces.ExternalApiService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace fromshot_api.Services.ExternalApiService.Authorizer
{
    public class AuthoraizerAuthService(AuthorizerHttpClient wrapper) : IAuthorizerGraphQLService
    {
        private readonly HttpClient _client = wrapper.Client;
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
            HttpResponseMessage response = await _client.PostAsync(_graphQLEndpoint, content);
            response.EnsureSuccessStatusCode();

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

    }
}
