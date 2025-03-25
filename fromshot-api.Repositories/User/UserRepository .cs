using fromshot_api.Domain.Interfaces;
using fromshot_api.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fromshot_api.Repositories.User
{
    public class UserRepository : IUserRepository
    {
        private static List<UserModel> _usuarios = new();

        public UserModel ObterPorId(int id)
        {
            return _usuarios.FirstOrDefault(u => u.Id == id);
        }

        public void Adicionar(UserModel usuario)
        {
            usuario.Id = _usuarios.Count + 1;
            _usuarios.Add(usuario);
        }
    }
}
