using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fromshot_api.Domain.DTOS.Authorizer
{
    public class AppDataRequest
    {
        public string Email { get; set; } = default!;
        public string[] Roles { get; set; } = default!;
        public string SteamId{ get; set; } = default!;
    }
}
