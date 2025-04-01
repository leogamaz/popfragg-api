﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fromshot_api.Domain.Interfaces.Repository
{
    public interface IAuthRepository
    {
        public Task<bool> SteamIdExisteAsync(string steamId);
        public Task<bool> NicknameExisteAsync(string nickname);
    }
}
