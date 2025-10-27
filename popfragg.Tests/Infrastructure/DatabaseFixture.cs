using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Testcontainers.PostgreSql;
using Xunit;

namespace popfragg.Tests.Infrastructure
{
    public class DatabaseFixture : IAsyncLifetime
    {
        private readonly PostgreSqlContainer _postgreSqlContainer = new PostgreSqlBuilder()
            .WithDatabase("popfragg_tests")
            .WithUsername("popfragg")
            .WithPassword("popfragg_password")
            .Build();
        public string ConnectionString => _postgreSqlContainer.GetConnectionString();
        //Incia o container antes de todos os testes
        public async Task InitializeAsync()
        {
            await _postgreSqlContainer.StartAsync();
        }

        //Limpa o container após todos os testes
        public async Task DisposeAsync()
        {
            await _postgreSqlContainer.StopAsync();
        }
    }
}
