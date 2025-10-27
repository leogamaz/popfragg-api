using popfragg.Domain.Models.Users;
using popfragg.Shared.DTOS.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace popfragg.Domain.Repositories
{
    public interface IAuthRepository
    {
        Task<User> AddAsync(User user);
        Task<User?> GetByEmailAsync(string email);
        Task<User?> GetBySteamIdAsync(string steamId);
        Task SaveChangesAsync();
        Task<UserConflictCheckResult> GetConflictsNewUser(SignUpRequestDTO dto);
    }
}
