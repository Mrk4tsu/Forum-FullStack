using Microsoft.AspNetCore.Identity;
using MrKatsuWebAPI.Application.Mail;
using MrKatsuWebAPI.Application.Tokens;
using MrKatsuWebAPI.DataAccess.Entities;
using MrKatsuWebAPI.DTO.ApiResponse;
using MrKatsuWebAPI.DTO.Authorize;
using MrKatsuWebAPI.Helper;
using Newtonsoft.Json.Linq;
using System.Net;

namespace MrKatsuWebAPI.Application.Systems
{
    public class AuthServices : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly TokenService _tokenService;
        private readonly IMailService _mailService;
        public AuthServices(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,
            IMailService mailService,
            TokenService tokenService)
        {
            _mailService = mailService;
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
                var token = await _tokenService.GenerateJwtToken(user!);
                return new ApiSuccessResult<TokenResponse>(token);
            }

            return new ApiErrorResult<TokenResponse>("Xác thực thất bại");
        }

        public async Task<ApiResult<string>> Register(RegisterModel model)
        {
            var user = new AppUser
            {
                UserName = model.Username,
                Email = model.Email,
                Avatar = $"assets/images/avatars/{model.AvatarIndex}.png",
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "USER");
                return new ApiSuccessResult<string>("Đăng kí thành công");
            }
            string[] errors = result.Errors.Select(e => e.Description).ToArray();
            return new ApiErrorResult<string>(errors);
        }
        public async Task<ApiResult<TokenResponse>> RefreshToken(string refreshToken)
        {
            var token = await _tokenService.RefreshTokenAsync(refreshToken);
            if(token == null)
            {
                return new ApiErrorResult<TokenResponse>("Refresh token không hợp lệ");
            }
            return new ApiSuccessResult<TokenResponse>(token);
        }

        public async Task<ApiResult<bool>> Logout(string refreshToken)
        {
            await _tokenService.RemoveToken(refreshToken);
            return new ApiSuccessResult<bool>(true);
        }

        public async Task<ApiResult<string>> RequestForgotPassword(MailRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.Username);
            if (user == null) return new ApiErrorResult<string>("User không tồn tại");
            if (request.Email != user.Email) return new ApiErrorResult<string>("Email yêu cầu khôi phục không chính xác");
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var url = UrlCallback(token, request.Username, "https://forum.gavl.io.vn", "confirm-password");
            var objects = new JObject
                            {
                                {"plink", url}
                            };
            await _mailService.SendMail(request.Email, $"Xác nhận khôi phục mật khẩu", SystemConstant.RESET_PASSWORD_TEMPLATE, objects);
            return new ApiSuccessResult<string>("Yêu cầu khôi phục mật khẩu đã được gửi đến email của bạn");
        }
        public async Task<ApiResult<bool>> ResetPassword(ForgotPasswordRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.Username);
            if (user == null) return new ApiErrorResult<bool>("User không tồn tại");
            var decodedToken = WebUtility.UrlDecode(request.Token);
            var result = await _userManager.ResetPasswordAsync(user, decodedToken, request.NewPassword);
            if (result.Succeeded)
            {
                return new ApiSuccessResult<bool>();
            }
            string[] errors = result.Errors.Select(e => e.Description).ToArray();
            return new ApiErrorResult<bool>(errors);
        }
        public async Task<ApiResult<bool>> ChangePassword(ChangePasswordRequest request, int userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null) return new ApiErrorResult<bool>("User không tồn tại");

            var result = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);
            if (result.Succeeded)
            {
                return new ApiSuccessResult<bool>();
            }
            string[] errors = result.Errors.Select(e => e.Description).ToArray();
            return new ApiErrorResult<bool>(errors);
        }
        private string UrlCallback(string token, string username, string domain, string page)
        {
            var encodedToken = WebUtility.UrlEncode(token);
            return $"{domain}/{page}?username={username}&token={encodedToken}";
        }
    }
}
