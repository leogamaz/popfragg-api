using popfragg.Domain.DTOS.Authorizer.Responses.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace popfragg.Domain.DTOS.Authorizer.Responses.SignUp
{
    public class SignUpResponse
    {
        public string Message { get; set; } = default!;
        public string Access_Token { get; set; } = default!;
        public int Expires_In { get; set; }
        public UserResponse User { get; set; } = default!;
    }
}
