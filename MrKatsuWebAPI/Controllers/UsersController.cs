using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MrKatsuWebAPI.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        [HttpGet("hello")]
        public IActionResult GetHello()
        {
            return Ok("Hello, World!");
        }
    }
}
