using Microsoft.EntityFrameworkCore;
using popfragg.Infrastructure.Data;
using popfragg.Tests.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace popfragg.Tests.Repositories
{
    public class NadesRepositorieTest(DatabaseFixture _databaseFixture) : IClassFixture<DatabaseFixture>
    {
        private readonly DatabaseFixture _databaseFixture = _databaseFixture;

        //Metodo alternativo para criar um novo Dbcontext para cada teste
        public RailwayContext CreateDbContext()
        {
           var options = NpgsqlDbContextOptionsBuilderExtensions.UseNpgsql(new DbContextOptionsBuilder<RailwayContext>(), _databaseFixture.ConnectionString)
                .Options;
            var context = new RailwayContext(options);
            context.Database.EnsureCreated();
            return context;
        }


    }
}
