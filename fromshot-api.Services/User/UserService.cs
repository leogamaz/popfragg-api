using fromshot_api.Domain.Interfaces;
using fromshot_api.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fromshot_api.Services.User
{
    public class UserService
    {
        private readonly IUserRepository _usuarioRepository;

        public UserService(IUserRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public UserModel ObterUsuario(int id)
        {
            return _usuarioRepository.ObterPorId(id);
        }

        public void CriarUsuario(string nome, string email)
        {
            var usuario = new UserModel { Nome = nome, Email = email };

            _usuarioRepository.Adicionar(usuario);
        }
    }
}
