using fromshot_api.Domain.Interfaces.Repository;
using Dapper;
using Npgsql;
using System.Threading.Tasks;
using System;
using fromshot_api.Common.Configurations;

namespace fromshot_api.Repositories.Auth
{
    public class AuthRepository(EnvironmentConfig connectionStrings) : IAuthRepository
    {
        private readonly string _authorizerConnection = connectionStrings.AuthorizerPublic;
        private readonly string _teste = connectionStrings.WriteDataBase;
        public async Task<bool> SteamIdExisteAsync(string steamId)
        {
            try
            {
                const string sql = @"
                SELECT COUNT(*)
                FROM authorizer_users
                WHERE app_data::jsonb ->> 'steam_id' = @steamid;
                ";

                await using var conn = new NpgsqlConnection(_authorizerConnection);
                var count = await conn.ExecuteScalarAsync<long>(sql, new { steamid = steamId });
                return count > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> NicknameExisteAsync(string nickname)
        {
            const string sql = @"
            SELECT COUNT(*)
            FROM authorizer_users
            WHERE app_data::jsonb ->> 'nickname' = @nickname;
        ";

            await using var conn = new NpgsqlConnection(_authorizerConnection);
            var count = await conn.ExecuteScalarAsync<long>(sql, new { nickname });
            return count > 0;
        }

        public async Task<bool> teste()
        {
            try
            {
                const string sql = @"
                SELECT COUNT(*)
                FROM log.logs
                ";

                await using var conn = new NpgsqlConnection(_teste);
                var count = await conn.ExecuteScalarAsync<long>(sql);
                return count > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
