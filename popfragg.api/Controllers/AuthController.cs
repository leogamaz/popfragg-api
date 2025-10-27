using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using popfragg.Api.Auth;
using popfragg.Domain.Models.Users;
using popfragg.Infrastructure.Environment;
using popfragg.Services;
using popfragg.Shared.DTOS.Auth;
using popfragg.Shared.DTOS.Steam;
using popfragg.Shared.Exceptions;
using popfragg.Shared.Helpers;
using System.Text;
using System.Threading.Tasks;
using popfragg.Domain.Services;

namespace popfragg.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(TokenService _tokenService, IAuthService _authService) : ControllerBase
    {
        [HttpPost("sign_in")]
        public async Task<IActionResult> SignIn([FromBody] SignInRequestDTO request)
        {
            var user =  await _authService.SignInAsync(request);
            var token = _tokenService.GenerateToken(user);
            Response.Cookies.Append("access_token", token, HttpRequests.SetCookieOptions(30));
            return Ok();
        }
        [HttpPost("sign_up")]
        public async Task<IActionResult> SignUp(SignUpRequestDTO newUser)
        {
            try
            {
                string? steamId = Request.Cookies["steamId"];
                if (!string.IsNullOrWhiteSpace(steamId))
                {
                    newUser.AddSteamId(steamId);
                }
                var user = await _authService.SignUpAsync(newUser);
                var token = _tokenService.GenerateToken(user);
                var responseDTO = await _authService.ConvertToSignUpResponseDto(user);

                Response.Cookies.Append("access_token", token, HttpRequests.SetCookieOptions(30));
                return CreatedAtAction(nameof(SignUp), new { id = responseDTO.Id }, responseDTO);
            }
            catch 
            {
                throw;
            }
        }

        [HttpGet("sign_in_steam")]
        public async Task<IActionResult> SignInWithSteam()
        {
            try
            {
                SteamAuthResponseDTO steamParams = HttpContext.Request.Query.ToSteamOpenIdResponse();

                User? user = await _authService.SignInWithSteam(steamParams);

                string steamId = steamParams.ClaimedId!.Replace("https://steamcommunity.com/openid/id/", "");
                var safeSteamId = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(steamId));

                if (user == null) //Se nulo, não tem cadastro, redireciona pro register
                {
                    // salva steamId no cookie para ser vinculado depois no registro
                    Response.Cookies.Append("steamId", steamId, HttpRequestsHelper.SetCookieOptions(15));
                    return Ok(new { newUser = true, safeSteamId });
                }
                var token = _tokenService.GenerateToken(user);  
                Response.Cookies.Append("access_token", token, HttpRequests.SetCookieOptions(30));

                return Ok(new { newUser = false });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

    }
}
