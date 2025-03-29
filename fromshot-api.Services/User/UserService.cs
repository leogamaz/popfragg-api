using fromshot_api.Domain.Interfaces.Repository;
using fromshot_api.Domain.Interfaces.Service;
using fromshot_api.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fromshot_api.Services.User
{
    public class UserService(IUserRepository usuarioRepository)  : IUserService
    {
        private readonly IUserRepository _usuarioRepository = usuarioRepository;

        //public UserModel ObterUsuario(int id)
        //{
        //    return _usuarioRepository.ObterPorId(id);
        //}

        public async Task<string> CriarUsuario()
        {
            return await _usuarioRepository.SignUp();
        }
    }
}
