using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace popfragg.Shared.DTOS.Auth
{
    public class UserConflictCheckResult
    {
        public bool SteamIdConflict { get; set; }
        public bool NicknameConflict { get; set; }
        public bool EmailConflict { get; set; }

        public bool HasAnyConflict =>
            SteamIdConflict ||
            NicknameConflict ||
            EmailConflict;
    }
}
