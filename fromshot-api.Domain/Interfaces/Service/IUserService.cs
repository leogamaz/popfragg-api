using fromshot_api.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fromshot_api.Domain.Interfaces.Service
{
    public interface IUserService
    {
        
        public Task<string> CriarUsuario();
    }
}
