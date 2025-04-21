using popfragg.Infrastructure.Configurations;
using popfragg.Domain.Interfaces.Common.DataBaseConnection;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace popfragg.Common.Repository.DataBaseConnection
{
    public class ReadOnlyDbConnection : IReadOnlyDbConnection
    {
        private readonly string _connectionString;

        public ReadOnlyDbConnection(EnvironmentConfig connectionStrings)
        {
            _connectionString = connectionStrings.ReadOnlyDatabase;
        }

        public async Task<T> WithConnection<T>(Func<IDbConnection, Task<T>> action)
        {
            await using var connection = new NpgsqlConnection(_connectionString);
            return await action(connection);
        }

        public async Task WithConnection(Func<IDbConnection, Task> action)
        {
            await using var connection = new NpgsqlConnection(_connectionString);
            await action(connection);
        }
    }
}
