using System.Diagnostics;
using fromshot_api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using fromshot_api.Domain.Models;
using fromshot_api.Domain.Interfaces.Service;
using fromshot_api.Helper;
using Supabase.Realtime.Converters;

namespace fromshot_api.Controllers
{
//    [Authorize] // Exige autenticação para todas as ações deste controller
    [Route("[controller]")] // Define a rota base com o nome do controller (Home)
    public class AuthController(IAuthService authService, IUserService userService) : ControllerBase
    {
        private readonly IAuthService _authService = authService;
        private readonly IUserService _userSerivce = userService;

        [HttpPost("sign_in_steam")]
        public async Task<IActionResult> SigInSteam([FromBody] OpenIdAuthModel steamParams)
        {
            //string steamId =  _authService.AuthSteam(steamParams)  
            string origin = Request.Headers["Origin"].ToString();

            string steamId = await _authService.AuthSteam(steamParams);
            
            Response.Cookies.Append("steamId", steamId, HttpRequestsHelper.SetCookieOptions(1));


            //UserModel user = _userService.FindUser(steamID)

            //if user null create

            //Login user

            return Ok(steamId);
        }
        [HttpGet("teste")]
        public async Task<IActionResult> teste()
        {
            Response.Cookies.Append("steamId", "o maldito do token", HttpRequestsHelper.SetCookieOptions(15));

            return Ok("stringgg");
        }

    }
}