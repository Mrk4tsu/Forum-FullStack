using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MrKatsuWebAPI.Application.Systems;
using MrKatsuWebAPI.Application.Tokens;
using MrKatsuWebAPI.DataAccess.Entities;
using MrKatsuWebAPI.DTO.Authorize;

namespace MrKatsuWebAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthorizesController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthorizesController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            var result = await _authService.Register(model);
            if (result.Success) return Ok(result);
            return BadRequest(result);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            var result = await _authService.Authorize(model);
            if(result.Success) return Ok(result);
            return BadRequest(result);
        }
    }
}
