using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace popfragg.Shared.DTOS.NadesDTO
{
    public class NadeDTO
    {
        public required string Title { get; set; }
        public required string Visibility { get; set; }
        public string? AccuracyLevel { get; set; }
        public required string Technique { get; set; }
        public string? Description { get; set; } = null;
        public required string Side { get; set; }
        public required string Map { get; set; }
        public required string Type { get; set; }

        [Url]
        public string? VideoUrl { get; set; } = null;
        public List<string>? Tags { get; set; } = [];
        public List<NadeImagesDTO>? Images { get; set; } = [];

        [JsonConverter(typeof(GuidListConverter))]
        public List<Guid>? Accesses { get; set; } = [];

    }
}
