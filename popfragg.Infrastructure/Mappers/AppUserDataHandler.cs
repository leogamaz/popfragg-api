using Dapper;
using popfragg.Domain.Entities;
using System.Data;
using System.Text.Json;

namespace popfragg.Infrastructure.Mappers
{
    public class AppUserDataHandler : SqlMapper.TypeHandler<AppUserData>
    {
        public override AppUserData? Parse(object value)
        {
            return JsonSerializer.Deserialize<AppUserData>(value.ToString() ?? "");
        }

        public override void SetValue(IDbDataParameter parameter, AppUserData? value)
        {
            parameter.Value = JsonSerializer.Serialize(value);
        }
    }
}
