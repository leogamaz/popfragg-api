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
using DotNetEnv;
using Microsoft.AspNetCore.WebUtilities;
using System.Text.Encodings.Web;
using System.Text;


namespace popfragg.Controllers
{
    //    [Authorize] // Exige autenticação para todas as ações deste controller!!
    [Route("[controller]")] // Define a rota base com o nome do controller (Home)
    public class AuthController(IAuthService authService, IJwtTokenService jwtTokenService, IWebHostEnvironment env) : ControllerBase
    {
        private readonly IAuthService _authService = authService;
        private readonly IJwtTokenService _jwtTokenService = jwtTokenService;
        private readonly string frontEndOrigin = Environment.GetEnvironmentVariable("FRONTEND_ORIGIN") ?? "http://localhost:4200";
        private readonly IWebHostEnvironment _webEnv = env;

        [HttpGet("sign_in_steam")]
        public async Task<IActionResult> SigInSteam()
        {

            SteamAuthOpenIdResponse steamParams = HttpContext.Request.Query.ToSteamOpenIdResponse();


            UserEntitie? user = await _authService.AuthSteam(steamParams);

            string steamId = steamParams.ClaimedId!.Replace("https://steamcommunity.com/openid/id/", "");
            var safeSteamId = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(steamId));

            if (user == null) //Se nulo, não tem cadastro, redireciona pro register
            {
                // salva steamId no cookie para ser vinculado depois no registro
                Response.Cookies.Append("steamId", steamId, HttpRequests.SetCookieOptions(15));
                //Pega o template html e faz replace com as variaveis
                var htmlPathRegister = Path.Combine(_webEnv.WebRootPath, "html", "steam-register-redirect.html");
                var templateRegister = await System.IO.File.ReadAllTextAsync(htmlPathRegister);
                
                var contentRegister = templateRegister
                    .Replace("{{steamId}}", safeSteamId)

                    .Replace("{{origin}}", frontEndOrigin);

                return Content(contentRegister, "text/html");
            }

            JwtClaims claims  = _jwtTokenService.GenerateClaims(user);
            string token = _jwtTokenService.GenerateToken(claims);

            Response.Cookies.Append("access_token", token, HttpRequests.SetCookieOptions(30));

            var htmlPathAuth = Path.Combine(_webEnv.WebRootPath, "html", "steam-redirect.html");
            var templateAuth = await System.IO.File.ReadAllTextAsync(htmlPathAuth);

            var contentAuth = templateAuth
                .Replace("{{steamId}}", safeSteamId)

                .Replace("{{origin}}", frontEndOrigin);

            return Content(contentAuth, "text/html");

        }

        [HttpPost("sign_up")]
        public async Task<IActionResult> SignUp([FromBody] SignUpRequest newUser)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            string? steamId = Request.Cookies["steamId"];

            if (!string.IsNullOrWhiteSpace(steamId))
            {
                newUser.AddSteamId(steamId);
            }


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