using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using fromshot_api;
using fromshot_api.Domain.Interfaces.Service;
using Moq;

namespace popfragg_api.Tests
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // Remove o AuthService real
                var descriptor = services.SingleOrDefault(d =>
                    d.ServiceType == typeof(IAuthService));

                if (descriptor is not null)
                    services.Remove(descriptor);

                // Adiciona um fake/mock
                var mock = new Mock<IAuthService>();
                mock.Setup(x => x.SignUp(It.IsAny<SignUpRequest>())).Returns(Task.CompletedTask);

                services.AddSingleton(mock.Object);
            });
        }
    }
}
