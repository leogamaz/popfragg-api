using popfragg.Domain.Models.Nades;
using popfragg.Shared.DTOS.NadesDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace popfragg.Domain.Services
{
    public interface INadesService
    {
        Task<Nades?> GetNadeByIdAsync(Guid id);
        Task<Nades> AddNadeAsync(NadeDTO newNade, Guid authorId);
        Task<IEnumerable<Nades>> GetAllNadesForUserAsync(Guid userId);
    }
}
