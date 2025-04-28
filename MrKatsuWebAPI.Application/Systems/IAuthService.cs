using MrKatsuWebAPI.DTO.ApiResponse;
using MrKatsuWebAPI.DTO.Authorize;

namespace MrKatsuWebAPI.Application.Systems
{
    public interface IAuthService
    {
        Task<ApiResult<TokenResponse>> Authorize(LoginModel model);
        Task<ApiResult<string>> Register(RegisterModel model);
    }
}