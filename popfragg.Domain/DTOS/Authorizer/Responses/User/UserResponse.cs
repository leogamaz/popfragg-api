using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace popfragg.Domain.DTOS.Authorizer.Responses.User
{
    public class UserResponse
    {
        public string Id { get; set; } = default!;
        public string Email { get; set; } = default!;
        public bool Email_Verified { get; set; }
        public string Given_Name { get; set; } = default!;
        public string Family_Name { get; set; } = default!;
        public string Nickname { get; set; } = default!;
        public string Preferred_Username { get; set; } = default!;
        public string Signup_Methods { get; set; } = default!;
        public bool Phone_Number_Verified { get; set; }
        public List<string> Roles { get; set; } = [];
        public long Created_At { get; set; }
        public long Updated_At { get; set; }
        public AppDataResponse App_Data { get; set; } = default!;
    }
}
