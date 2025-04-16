using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace popfragg.Domain.DTOS.User
{
    public class UserConflictCheckResult
    {
        public string? SteamIdConflict { get; set; }
        public string? NicknameConflict { get; set; }
        public string? EmailConflict { get; set; }

        public bool HasAnyConflict =>
            !string.IsNullOrEmpty(SteamIdConflict) ||
            !string.IsNullOrEmpty(NicknameConflict) ||
            !string.IsNullOrEmpty(EmailConflict);
    }
}
