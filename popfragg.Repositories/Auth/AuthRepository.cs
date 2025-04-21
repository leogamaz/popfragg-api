using popfragg.Domain.Interfaces.Repository;
using Dapper;
using Npgsql;
using System.Threading.Tasks;
using System;
using popfragg.Infrastructure.Configurations;
using popfragg.Infrastructure.Repository;
using popfragg.Common.Exceptions;
using popfragg.Domain.DTOS.Authorizer.Requests;
using popfragg.Domain.DTOS.User;
using System.Collections.Generic;

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
                var count = await conn.ExecuteScalarAsync<long>(sql, new { steamId });
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

        public async Task<UserConflictCheckResult> CheckNewUserAsync(SignUpRequest request)
        {
            try
            {
                var whereClauses = new List<string>();
                var parameters = new DynamicParameters();

                if (!string.IsNullOrWhiteSpace(request.AppData?.SteamId))
                {
                    whereClauses.Add("CASE WHEN EXISTS(SELECT 1 FROM authorizer_users WHERE LOWER(app_data::jsonb ->> 'steam_id') = LOWER(@steamId)) THEN 'SteamId' ELSE NULL END AS SteamIdConflict");
                    parameters.Add("steamId", request.AppData.SteamId);
                }
                else
                {
                    whereClauses.Add("NULL AS SteamIdConflict");
                }

                whereClauses.Add("CASE WHEN EXISTS(SELECT 1 FROM authorizer_users WHERE LOWER(nickname) = LOWER(@nickname)) THEN 'Nickname' ELSE NULL END AS NicknameConflict");
                parameters.Add("nickname", request.Nickname);

                whereClauses.Add("CASE WHEN EXISTS(SELECT 1 FROM authorizer_users WHERE LOWER(email) = LOWER(@email)) THEN 'Email' ELSE NULL END AS EmailConflict");
                parameters.Add("email", request.Email);

                var sql = $"SELECT {string.Join(",\n", whereClauses)};";

                await using var conn = new NpgsqlConnection(AuthorizerPublic);
                var result = await conn.QuerySingleAsync<UserConflictCheckResult>(sql, parameters);
                return result;

            }
            catch (Exception)
            {
                throw new InfrastructureUnavailableException("Erro na validação do novo usuário");
            }
        }

    }
}
