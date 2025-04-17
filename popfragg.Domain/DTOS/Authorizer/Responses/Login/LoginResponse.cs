using popfragg.Domain.DTOS.Authorizer.Responses.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace popfragg.Domain.DTOS.Authorizer.Responses.Login
{
    public class LoginResponse
    {
        public required UserResponse User { get; set; }
        public required string AccessToken { get; set; }
        public required int ExpiresIn { get; set; }
        public string? Message { get; set; }
    }
}
