using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace popfragg.Shared.DTOS.Auth
{
    public class SignUpResponseDTO
    {
        public string Id { get; set; } = default!;
        public string? SteamId { get; set; }
        public string Nickname { get; set; } = default!;
        public string? AvatarUrl { get; set; }
        public string? ProfileUrl { get; set; }
        public string Email { get; set; } = default!;
        public string GivenName { get; set; } = default!;
        public string FamilyName { get; set; } = default!;
        public string Role { get; set; } = "";
        public DateTime Created_At { get; set; }
    }
}
