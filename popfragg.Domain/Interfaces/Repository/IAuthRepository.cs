using popfragg.Domain.DTOS.Authorizer.Requests;
using popfragg.Domain.DTOS.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace popfragg.Domain.Interfaces.Repository
{
    public interface IAuthRepository
    {
        public Task<bool> SteamIdExisteAsync(string steamId);
        public Task<bool> NicknameExisteAsync(string nickname);
        public Task<bool> EmailExisteAsync(string email);
        public Task<UserConflictCheckResult> CheckNewUserAsync(SignUpRequest request);

        
    }
}
