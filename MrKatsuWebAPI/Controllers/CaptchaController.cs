using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MrKatsuWebAPI.Application.Systems.Cloudflares;
using MrKatsuWebAPI.DTO.Captcha;

namespace MrKatsuWebAPI.Controllers
{
    [Route("api/captcha")]
    [ApiController]
    public class CaptchaController : BaseController
    {
        private readonly ICloudflareTurnstileService _turnstileService;

        public CaptchaController(ICloudflareTurnstileService turnstileService)
        {
            _turnstileService = turnstileService;
        }
        [HttpPost("verify")]
        public async Task<IActionResult> Verify([FromBody] TurnstileVerifyRequest request)
        {
            var isValid = await _turnstileService.VerifyTokenAsync(request.Token);

            if (!isValid)
            {
                return Error("Captcha verification failed. Please try again.");
            }

            return Success("Xác thực thành công", true);
        }
    }
}
