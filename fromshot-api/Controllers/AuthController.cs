using System.Diagnostics;
using fromshot_api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using fromshot_api.Domain.Interfaces.Service;
using fromshot_api.Helper;
using fromshot_api.Common.Repository;
using fromshot_api.Domain.DTOS.Authorizer;
using fromshot_api.Domain.DTOS.Steam;

namespace fromshot_api.Controllers
{
    //    [Authorize] // Exige autenticação para todas as ações deste controller
    [Route("[controller]")] // Define a rota base com o nome do controller (Home)
    public class AuthController(IAuthService authService, ConnectionStrings connectionStrings) : ControllerBase
    {
        private readonly IAuthService _authService = authService;
        private readonly ConnectionStrings _connectionStrings = connectionStrings;  

        [HttpPost("sign_in_steam")]
        public async Task<IActionResult> SigInSteam([FromBody] OpenIdAuth steamParams)
        {
            //string steamId =  _authService.AuthSteam(steamParams)  

            string steamId = await _authService.AuthSteam(steamParams);
            
            Response.Cookies.Append("steamId", steamId, HttpRequests.SetCookieOptions(1));
         
            //UserModel user = _userService.FindUser(steamID)

            //if user null create

            //Login user

            return Ok(steamId);
        }
        [HttpPost("sign_up_steam")]
        public async Task<IActionResult> SignUpWithSteam([FromBody] SignUpSteamRequest newUser)
        {
            string user = await _authService.SignUpWithSteam(newUser);
            Console.WriteLine(user);

            Response.Cookies.Append("steamId", "o maldito do token", HttpRequests.SetCookieOptions(15));
            string conn = _connectionStrings.Authorizer;
            return Ok(conn);
        }

    }
}