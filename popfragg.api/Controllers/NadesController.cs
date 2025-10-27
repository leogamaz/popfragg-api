using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using popfragg.Domain.Services;
using popfragg.Shared.DTOS.NadesDTO;
using System.Security.Claims;

namespace popfragg.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class NadesController(INadesService _nadesService, ICurrentUserService _currentUser) : ControllerBase
    {
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetNadeById(string id)
        {
            Guid nadeId = Guid.Parse(id);
            var nade = await _nadesService.GetNadeByIdAsync(nadeId);
            return nade is null ? NotFound() : Ok(nade);
        }
        [HttpPost("create")]
        public async Task<IActionResult> CreateNade([FromBody] NadeDTO newNade)
        {
            var authorId = _currentUser.Id;
            

            if (string.IsNullOrEmpty(authorId))
                return BadRequest("Author ID is missing.");

            //var authorIdClaim = "019a190e-6573-7bc5-809a-2a79fda53073";
            var guid = Guid.Parse(authorId);
            var nade = await _nadesService.AddNadeAsync(newNade, guid);
            return Ok(nade);
        }
        [HttpGet("user/nades")]
        public async Task<IActionResult> GetAllNadesForUser()
        {
            var userId = _currentUser.Id;
            if (string.IsNullOrEmpty(userId))
                return BadRequest("User ID is missing.");

            var guid = Guid.Parse(userId);
            var nades = await _nadesService.GetAllNadesForUserAsync(guid);
            return Ok(nades);
        }
    }
}
