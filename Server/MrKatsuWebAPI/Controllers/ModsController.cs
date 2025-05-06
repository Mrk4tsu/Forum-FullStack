using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MrKatsuWebAPI.Application.Mods;
using MrKatsuWebAPI.DTO.Mods;
using MrKatsuWebAPI.DTO.Paging;

namespace MrKatsuWebAPI.Controllers
{
    [Route("api/mod")]
    [ApiController]
    public class ModsController : BaseController
    {
        private readonly IModService _modService;
        public ModsController(IModService modService)
        {
            _modService = modService;
        }
        [HttpGet("all")]
        public async Task<IActionResult> GetAllMods([FromQuery] ModPagingRequest request)
        {
            var result = await _modService.GetMods(request);
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetModById(int id, [FromQuery] PagingRequest request)
        {
            var result = await _modService.GetModById(id, request);
            return Ok(result);
        }
        [HttpGet("internal/{id}")]
        public async Task<IActionResult> GetModInternalById(int id)
        {
            var result = await _modService.GetModInternalById(id);
            return Ok(result);
        }
        [HttpGet("{modId}/reacts")]
        public async Task<IActionResult> GetReactByModId(int modId, [FromQuery] PagingRequest request)
        {
            var result = await _modService.GetReactByModId(modId, request);
            return Ok(result);
        }
        [HttpPost("create"), Authorize]
        public async Task<IActionResult> CreateMod([FromBody] ModCombineRequest request)
        {
            var userId = GetUserIdFromClaims();
            if (userId == null) return Unauthorized("Unauthorized");
            var result = await _modService.CreateMod(request, userId.Value);
            return Ok(result);
        }
        [HttpPut("update-mod"), Authorize]
        public async Task<IActionResult> UpdateMod(int id, [FromBody] ModUpdateCombineRequest request)
        {
            var userId = GetUserIdFromClaims();
            if (userId == null) return Unauthorized("Unauthorized");
            var result = await _modService.UpdateMod(id, request, userId.Value);
            return Ok(result);
        }
        [HttpDelete("delete-url"), Authorize]
        public async Task<IActionResult> DeleteUrls(int modId, [FromBody] UrlDeleteRequest request)
        {
            var userId = GetUserIdFromClaims();
            if (userId == null) return Unauthorized("Unauthorized");
            var result = await _modService.DeleteUrls(modId, request, userId.Value);
            return Ok(result);
        }
        [HttpDelete("delete-mod"), Authorize]
        public async Task<IActionResult> DeleteMod(int id)
        {
            var userId = GetUserIdFromClaims();
            if (userId == null) return Unauthorized("Unauthorized");
            var result = await _modService.DeleteMod(id, userId.Value);
            return Ok(result);
        }
        [HttpPost("create-reaction"), Authorize]
        public async Task<IActionResult> CreateReaction([FromBody] ReactionRequest request)
        {
            var userId = GetUserIdFromClaims();
            if (userId == null) return Unauthorized("Unauthorized");
            var result = await _modService.CreateReaction(request, userId.Value);
            return Ok(result);
        }
        [HttpPut("update-reaction"), Authorize]
        public async Task<IActionResult> UpdateReaction(int id, [FromBody] ReactionRequest request)
        {
            var userId = GetUserIdFromClaims();
            if (userId == null) return Unauthorized("Unauthorized");
            var result = await _modService.UpdateReaction(id, request, userId.Value);
            return Ok(result);
        }
        [HttpDelete("delete-reaction"), Authorize]
        public async Task<IActionResult> DeleteReaction(int id)
        {
            var userId = GetUserIdFromClaims();
            if (userId == null) return Unauthorized("Unauthorized");
            var result = await _modService.DeleteReaction(id, userId.Value);
            return Ok(result);
        }
    }
}
