using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MrKatsuWebAPI.Application.Tokens;
using MrKatsuWebAPI.DataAccess.Entities;
using MrKatsuWebAPI.DTO.Authorize;

namespace MrKatsuWebAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthorizesController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly TokenService _tokenService;
        public AuthorizesController(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        TokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            var user = new ApplicationUser
            {
                UserName = model.Username,
                Email = model.Email,
                FullName = model.FullName
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                return Ok(new { Message = "User registered successfully" });
            }

            return BadRequest(result.Errors);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, false, false);
            if (result.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(model.Username);
                var token = _tokenService.GenerateJwtToken(user);
                return Ok(new { Token = token });
            }

            return Unauthorized(new { Message = "Invalid login attempt" });
        }
    }
}
