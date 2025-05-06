using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MrKatsuWebAPI.Application.Systems;
using MrKatsuWebAPI.Application.Systems.Cloudflares;
using MrKatsuWebAPI.DTO.Authorize;

namespace MrKatsuWebAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthorizesController : BaseController
    {
        private readonly IAuthService _authService;
        private readonly ICloudflareTurnstileService _turnstileService;
        public AuthorizesController(IAuthService authService, ICloudflareTurnstileService turnstileService)
        {
            _turnstileService = turnstileService;
            _authService = authService;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            var captchaValid = await _turnstileService.VerifyTokenAsync(model.CaptchaToken);
            if (!captchaValid) return Error("Captcha validation failed. Please try again.");
            var result = await _authService.Register(model);
            if (result.Success) return Ok(result);
            return BadRequest(result);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            var result = await _authService.Authorize(model);
            if (result.Success) return Ok(result);
            return BadRequest(result);
        }
        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken(string refreshToken)
        {
            var result = await _authService.RefreshToken(refreshToken);
            if (result.Success) return Ok(result);
            return BadRequest(result);
        }
        [HttpPost("logout")]
        public async Task<IActionResult> Logout(string refreshToken)
        {
            var result = await _authService.Logout(refreshToken);
            if (result.Success) return Ok(result);
            return BadRequest(result);
        }
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(MailRequest request)
        {
            var result = await _authService.RequestForgotPassword(request);
            if (result.Success) return Ok(result);
            return BadRequest(result);
        }
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ForgotPasswordRequest request)
        {
            var result = await _authService.ResetPassword(request);
            return Ok(result);
        }
        [HttpPost("change-password"), Authorize]
        public async Task<IActionResult> ChangePassword(ChangePasswordRequest request)
        {
            var userId = GetUserIdFromClaims();
            if (userId == null) return Unauthorized("User ID not found in claims."); 
            var result = await _authService.ChangePassword(request, userId.Value);
            if (result.Success) return Ok(result);
            return BadRequest(result);
        }
    }
}
