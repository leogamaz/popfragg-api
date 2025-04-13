using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace popfragg.Common.Helpers
{
    public static class Json
    {
        private static readonly JsonSerializerSettings _snakeCaseSettings = new()
        {
            ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new SnakeCaseNamingStrategy()
            }
        };

        public static string SerializeSnakeCase(object obj)
        {
            return JsonConvert.SerializeObject(obj, _snakeCaseSettings);
        }
        public static T DeserializeSnakeCase<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json, _snakeCaseSettings)!;
        }
    }
}
