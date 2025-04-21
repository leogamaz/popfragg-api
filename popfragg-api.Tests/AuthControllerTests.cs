using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using fromshot_api;
using FluentAssertions;

namespace popfragg_api.Tests
{
    public class AuthControllerTests(WebApplicationFactory<Program> factory) : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client = factory.CreateClient();

        [Fact]
        public async Task SignUp_DeveRetornarOk()
        {
            // Arrange
            var request = new
            {
                email = "teste@email.com",
                password = "123456"
            };

            // Act
            var response = await _client.PostAsJsonAsync("/auth/sign_up", request);

            // Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }
    }
}