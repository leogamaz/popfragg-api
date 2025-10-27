using popfragg.Domain.Models.Nades;
using popfragg.Domain.Models.Users;
using popfragg.Shared.DTOS.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace popfragg.Domain.Repositories
{
    public interface INadesRepository
    {
        Task<Nades> AddAsync(Nades nade);
        Task<Nades?> GetById(Guid id);
        Task<IEnumerable<Nades>> GetAllNadesForUserAsync(Guid userId);
        Task SaveChangesAsync();
    }
}
