using System.Diagnostics;
using fromshot_api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using fromshot_api.Domain.Interfaces.Service;
using fromshot_api.Helper;
using fromshot_api.Domain.DTOS.Steam;
using fromshot_api.Common.Configurations;
using fromshot_api.Domain.DTOS.Authorizer.Requests;
using fromshot_api.Domain.DTOS.Authorizer.Responses;

namespace fromshot_api.Controllers
{
    //    [Authorize] // Exige autenticação para todas as ações deste controller
    [Route("[controller]")] // Define a rota base com o nome do controller (Home)
    public class AuthController(IAuthService authService) : ControllerBase
    {
        private readonly IAuthService _authService = authService;

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
        public async Task<IActionResult> SignUpWithSteam([FromBody] SignUpRequest newUser)
        {

            var user = await _authService.SignUpWithSteam(newUser);
            Console.WriteLine(user);

            Response.Cookies.Append("steamId", "o maldito do token", HttpRequests.SetCookieOptions(15));
            return Ok();
        }

        [HttpPost("sign_up")]
        public async Task<IActionResult> SignUp([FromBody] SignUpRequest newUser)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            SignUpResponse user =  await _authService.SignUp(newUser);
            Response.Cookies.Append("access_token",user.Access_Token, HttpRequests.SetCookieOptions(15));

            return Ok(user.User);
        }

    }
}