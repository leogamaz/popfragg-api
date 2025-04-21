using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Constants =  popfragg.Common.Constants;




namespace popfragg.Domain.DTOS.Authorizer.Requests
{
    public class SignUpRequest
    {
        public required string Email { get; set; } = default!;
        public required string Password { get; set; } = default!;
        public required string ConfirmPassword { get; set; } = default!;
        public required string Nickname { get; set; } = default!;
        public required string GivenName { get; set; }
        public required string FamilyName { get; set; }
        public string? Gender { get; set; }
        public string? Birthdate { get; set; }
        public string? PhoneNumber { get; set; }
        public List<string> Roles { get; private set; } = [];
        public AppDataRequest? AppData { get; private set; } = default!;

        public void AddCommonRole()
        {
            Roles.Add(Constants.Roles.Common);
        }
        public void AddSteamId(string steamId)
        {
            AppData ??= new AppDataRequest();
            AppData.SteamId = steamId;
        }

    }
}
