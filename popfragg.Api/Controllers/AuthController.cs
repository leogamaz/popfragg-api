using System.Diagnostics;
using popfragg.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using popfragg.Domain.Interfaces.Service;
using popfragg.Helper;
using popfragg.Domain.DTOS.Steam;
using popfragg.Domain.DTOS.Authorizer.Requests;
using popfragg.Domain.DTOS.Authorizer.Responses.SignUp;
using popfragg.Domain.DTOS.Authorizer.Responses.Login;
using popfragg.Helper.Mappers;
using popfragg.Domain.Entities;
using popfragg.Domain.Helpers;

namespace popfragg.Controllers
{
    //    [Authorize] // Exige autenticação para todas as ações deste controller!!
    [Route("[controller]")] // Define a rota base com o nome do controller (Home)
    public class AuthController(IAuthService authService, IJwtTokenService jwtTokenService) : ControllerBase
    {
        private readonly IAuthService _authService = authService;
        private readonly IJwtTokenService _jwtTokenService = jwtTokenService;

        [HttpGet("sign_in_steam")]
        public async Task<IActionResult> SigInSteam()
        {

            SteamAuthOpenIdResponse steamParams = HttpContext.Request.Query.ToSteamOpenIdResponse();


            UserEntitie user = await _authService.AuthSteam(steamParams);

            JwtClaims claims  = _jwtTokenService.GenerateClaims(user);
            string token = _jwtTokenService.GenerateToken(claims);

            Response.Cookies.Append("access_token", token, HttpRequests.SetCookieOptions(30));
            Response.Cookies.Append("steamId", user!.AppData!.SteamId!, HttpRequests.SetCookieOptions(30));

            var html = $@"
                <html>
                  <body>
                    <script>
                      window.opener.postMessage({{steamId: '{user.AppData.SteamId}' }}, 'http://localhost:4200/register');
                      window.close();
                    </script>
                    <p>Autenticando com Steam... Você será redirecionado.</p>
                  </body>
                </html>
                ";

            return Content(html, "text/html");
        }
        [HttpPost("sign_up_steam")]
        public async Task<IActionResult> SignUpWithSteam([FromBody] SignUpRequest newUser)
        {

            var user = await _authService.SignUpWithSteam(newUser);
            Console.WriteLine(user);

            Response.Cookies.Append("steamId", "access_token", HttpRequests.SetCookieOptions(15));
            return Ok();
        }

        [HttpPost("sign_up")]
        public async Task<IActionResult> SignUp([FromBody] SignUpRequest newUser)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            SignUpResponse user =  await _authService.SignUp(newUser);

            Response.Cookies.Append("access_token",user.Access_Token, HttpRequests.SetCookieOptions(30));

            return Ok(user.User);
        }

        [HttpPost("sign_in")]
        public async Task<IActionResult> SignIn([FromBody] SignInRequest signIn)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            LoginResponse response = await _authService.SignIn(signIn);

            Response.Cookies.Append("access_token",response.AccessToken,HttpRequests.SetCookieOptions(30));

            return Ok(response);
        }

    }
}