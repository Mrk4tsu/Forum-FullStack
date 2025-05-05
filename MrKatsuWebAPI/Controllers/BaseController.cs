using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MrKatsuWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected int? GetUserIdFromClaims()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (int.TryParse(userIdClaim.Value, out int userId))
            {
                return userId;
            }

            return null;
        }
        public static IActionResult Success(string? message = null, object? data = null)
        {
            return new OkObjectResult(new
            {
                success = true,
                message,
                data
            });
        }
        public static IActionResult Success(object? data = null)
        {
            return new OkObjectResult(new
            {
                success = true,
                data
            });
        }
        public static IActionResult Error(string? message = null, Code statusCode = Code.BadRequest, object? errors = null)
        {
            return new ObjectResult(new
            {
                success = false,
                message,
                errors
            })
            {
                StatusCode = (int)statusCode
            };
        }
        public static IActionResult NotFound(string? message = null, object? errors = null)
        {
            return new ObjectResult(new
            {
                success = false,
                message,
                errors
            })
            {
                StatusCode = Code.NotFound.GetHashCode()
            };
        }
        public static IActionResult BadRequest(string? message = null, object? errors = null)
        {
            return new ObjectResult(new
            {
                success = false,
                message,
                errors
            })
            {
                StatusCode = Code.BadRequest.GetHashCode()
            };
        }
        public enum Code
        {
            Ok = 200,
            Created = 201,
            NoContent = 204,
            BadRequest = 400,
            Unauthorized = 401,
            Forbidden = 403,
            NotFound = 404,
            Conflict = 409,
            InternalServerError = 500,
            GatewayTimeout = 504
        }
    }
}
