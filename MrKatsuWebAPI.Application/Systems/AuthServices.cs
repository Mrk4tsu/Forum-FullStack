using Microsoft.AspNetCore.Identity;
using MrKatsuWebAPI.Application.Tokens;
using MrKatsuWebAPI.DataAccess.Entities;
using MrKatsuWebAPI.DTO.ApiResponse;
using MrKatsuWebAPI.DTO.Authorize;

namespace MrKatsuWebAPI.Application.Systems
{
    public class AuthServices : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly TokenService _tokenService;
        public AuthServices(UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signInManager,TokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        public async Task<ApiResult<TokenResponse>> Authorize(LoginModel model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, false, false);
            if (result.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(model.Username);
                var token = _tokenService.GenerateJwtToken(user);
                return new ApiSuccessResult<TokenResponse>(token);
            }

            return new ApiErrorResult<TokenResponse>("Xác thực thất bại");
        }

        public async Task<ApiResult<string>> Register(RegisterModel model)
        {
            var user = new ApplicationUser
            {
                UserName = model.Username,
                Email = model.Email,
                Profile = new Profile {
                    DisplayName = model.DisplayName,
                    AvatarUrl = "assets/images/avatars/519.png"
                }
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                return new ApiSuccessResult<string>("Đăng kí thành công");
            }
            return new ApiErrorResult<string>(result.Errors.ToArray().ToString()!);
        }
    }
}
