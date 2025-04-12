using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fromshot_api.Domain.DTOS.Authorizer.Responses
{
    public class AppDataResponse
    {
        public string Email { get; set; } = default!;
        public List<string> Roles { get; set; } = [];
        public string Steam_Id { get; set; } = default!;
    }
}
