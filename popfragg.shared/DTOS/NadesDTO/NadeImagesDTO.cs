using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace popfragg.Shared.DTOS.NadesDTO
{
    public class NadeImagesDTO
    {
        [Url]
        public required string ImageUrl { get; set; }
        public required int OrderImage { get; set; }
    }
}
