using Microsoft.EntityFrameworkCore;
using popfragg.Domain.Models.Nades;
using popfragg.Domain.Repositories;
using popfragg.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace popfragg.Infrastructure.Repositories.NadesRepository
{
    public class NadesRepository(AppDbContext _context) : INadesRepository
    {
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        public async Task<Nades> AddAsync(Nades nade)
        {
            await _context.Nades.AddAsync(nade);
            await _context.SaveChangesAsync();
            return nade;
        }
        public async Task<Nades?> GetById(Guid id)
        {
            return await _context.Nades
                .Include(n => n.Images)
                .Include(n => n.Tags)
                .Include(n => n.Accesses)
                .FirstOrDefaultAsync(n => n.Id == id);
            
        }
        public async Task<IEnumerable<Nades>> GetAllNadesForUserAsync(Guid userId)
        {
            return await _context.Nades
                .Where(n => n.AuthorId == userId || n.Accesses.Any(a => a.UserId == userId) || n.Visibility == "public")
                .Include(n => n.Images)
                .Include(n => n.Tags)
                .Include(n => n.Accesses)
                .ToListAsync();
        }
    }
}
