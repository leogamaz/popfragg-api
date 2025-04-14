using popfragg.Domain.Interfaces.Repository;
using Dapper;
using Npgsql;
using System.Threading.Tasks;
using System;
using popfragg.Common.Configurations;
using popfragg.Common.Repository;
using System.Net.Http;
using popfragg.Common.Http;
using popfragg.Common.Helpers.Querys;
using System.Text;
using popfragg.Common.Exceptions;

namespace popfragg.Repositories.Auth
{
    public class AuthRepository(EnvironmentConfig config) : BaseRepository(config),IAuthRepository 
    {
        public async Task<bool> SteamIdExisteAsync(string steamId)
        {
            try
            {
                const string sql = @"
                SELECT COUNT(*)
                FROM authorizer_users
                WHERE LOWER(app_data::jsonb ->> 'steam_id') = LOWER(@steamId);
                ";

                await using var conn = new NpgsqlConnection(AuthorizerPublic);
                var count = await conn.ExecuteScalarAsync<long>(sql, new { steamId = steamId });
                return count != 0;
            }
            catch (Exception)
            {
                throw new InfrastructureUnavailableException("Erro ao validar steam_id");
            }
        }

        public async Task<bool> NicknameExisteAsync(string nickname)
        {
            try
            {
                const string sql = @"
                SELECT COUNT(*)
                FROM authorizer_users
                WHERE LOWER(nickname) = LOWER(@nickname);
                ";

                await using var conn = new NpgsqlConnection(AuthorizerPublic);
                var count = await conn.ExecuteScalarAsync<long>(sql, new { nickname });
                return count != 0;
            }
            catch (Exception)
            {
                throw new InfrastructureUnavailableException("Erro ao validar nickname");
            }
        }

        public async Task<bool> teste()
        {
            try
            {
                const string sql = @"
                SELECT COUNT(*)
                FROM log.logs
                ";

                await using var conn = new NpgsqlConnection(WriteDb);
                var count = await conn.ExecuteScalarAsync<long>(sql);
                return count > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> EmailExisteAsync(string email)
        {
            try
            {
                const string sql = @"
                SELECT COUNT(*)
                FROM authorizer_users
                WHERE LOWER(email) = LOWER(@email);
                ";

                await using var conn = new NpgsqlConnection(AuthorizerPublic);
                var count = await conn.ExecuteScalarAsync<long>(sql, new { email });
                return count != 0;
            }
            catch (Exception)
            {
                throw new InfrastructureUnavailableException("Erro ao validar email");
            }
        }

    }
}
