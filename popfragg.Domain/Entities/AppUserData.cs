using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace popfragg.Domain.Entities
{
    public class AppUserData
    {
        [JsonPropertyName("steam_id")]
        public string? SteamId { get; init; }
    }
}
