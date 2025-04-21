using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace popfragg.Domain.DTOS.Authorizer.Requests
{
    public class SignInRequest
    {
        public required string Email { get; set; } = default!;
        public required string Password { get; set; } = default!;
    }
}
