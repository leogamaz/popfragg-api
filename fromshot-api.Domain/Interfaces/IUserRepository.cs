using fromshot_api.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fromshot_api.Domain.Interfaces
{
    public interface IUserRepository
    {
        UserModel ObterPorId(int id);
        void Adicionar(UserModel usuario);
    }
}
