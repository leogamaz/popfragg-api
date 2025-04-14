using popfragg.Domain.DTOS.Authorizer.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace popfragg.Domain.DTOS.Steam
{
    public class SignUpSteamRequest
    {
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string ConfirmPassword { get; set; } = default!;
        public string[] Scope { get; set; } = default!;
        public string Nickname { get; set; } = default!;
        public string? GivenName { get; set; }
        public string? FamilyName { get; set; }
        public string? Gender { get; set; }
        public string? Birthdate { get; set; }
        public string? PhoneNumber { get; set; }
        public AppDataRequest AppData { get; set; } = default!;
    }
}
