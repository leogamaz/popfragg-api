using fromshot_api.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fromshot_api.Domain.Interfaces.Repository
{
    public interface IUserRepository
    {
        
        public Task<string> SignUp();
    }
}
