//using Microsoft.AspNetCore.Mvc;
//using popfragg.Services.User;
//using popfragg.Domain.Models;

//namespace popfragg.Controllers.User
//{
//    public class UserController : ControllerBase
//    {
//        private readonly UserService _usuarioService;

//        public UserController(UserService usuarioService)
//        {
//            _usuarioService = usuarioService;
//        }

//        [HttpGet("{id}")]
//        public IActionResult Get(int id)
//        {
//            var usuario = _usuarioService.ObterUsuario(id);
//            if (usuario == null) return NotFound();
//            return Ok(usuario);
//        }

//        [HttpPost("sign_in")]
//        public IActionResult Post([FromBody] UserModel usuario)
//        {
//            //_usuarioService.CriarUsuario(usuario.Nome, usuario.Email);
//            return Created("", usuario);
//        }
//    }
//}
