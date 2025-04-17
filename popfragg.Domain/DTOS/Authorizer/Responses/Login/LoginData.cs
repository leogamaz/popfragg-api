using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace popfragg.Domain.DTOS.Authorizer.Responses.Login
{
    public class LoginData
    {
        public LoginResponse Login { get; set; } = default!;
    }
}
