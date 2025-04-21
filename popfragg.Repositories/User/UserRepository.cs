using popfragg.Domain.Entities;
using System;
using System.Threading.Tasks;
using popfragg.Domain.Interfaces.Repository;
using popfragg.Infrastructure.Repository;
using popfragg.Infrastructure.Configurations;
using Npgsql;
using popfragg.Common.Exceptions;
using Dapper;



namespace popfragg.Repositories.User
{
    public class UserRepository(EnvironmentConfig config) : BaseRepository(config),  IUserRepository
    {
        public async Task<UserEntitie> GetUserBySteamId(string steamId)
        {
            try
            {
                const string sql = @"
                    SELECT *
                    FROM authorizer_users
                    WHERE LOWER(app_data::jsonb ->> 'steam_id') = LOWER(@steamId)
                    LIMIT 1
                ";
                await using var conn = new NpgsqlConnection(AuthorizerPublic);
                var user = await conn.QueryFirstOrDefaultAsync<UserEntitie>(sql, new { steamId });
                return user;

            }
            catch (Exception)
            {
                throw new InfrastructureUnavailableException("Erro ao validar steam_id");
            }
        }
    }
}
