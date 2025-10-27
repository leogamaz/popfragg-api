using Microsoft.EntityFrameworkCore;
using popfragg.Domain.Repositories;
using popfragg.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using popfragg.Shared.DTOS.Auth;
using popfragg.Domain.Models.Users;

namespace popfragg.Infrastructure.Repositories.Auth
{
    public class AuthRepository(RailwayContext _context) : IAuthRepository
    {
        public async Task<User> AddAsync(User user)
        {

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;

        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }
        public async Task<User?> GetBySteamIdAsync(string steamId)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.SteamId == steamId);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        public async Task<UserConflictCheckResult> GetConflictsNewUser(SignUpRequestDTO dto)
        {
            var emailExists = await _context.Users.AnyAsync(u => u.Email == dto.Email);
            var steamIdExists = !string.IsNullOrWhiteSpace(dto.SteamId) && await _context.Users.AnyAsync(u => u.SteamId == dto.SteamId);
            var nicknameExists = !string.IsNullOrWhiteSpace(dto.Nickname) && await _context.Users.AnyAsync(u => u.Nickname == dto.Nickname);
            return new UserConflictCheckResult
            {
                EmailConflict = emailExists,
                SteamIdConflict = steamIdExists,
                NicknameConflict = nicknameExists
            };
        }
    }
}
