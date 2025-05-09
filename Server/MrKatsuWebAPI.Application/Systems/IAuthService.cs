﻿using MrKatsuWebAPI.DTO.ApiResponse;
using MrKatsuWebAPI.DTO.Authorize;

namespace MrKatsuWebAPI.Application.Systems
{
    public interface IAuthService
    {
        Task<ApiResult<TokenResponse>> Authorize(LoginModel model);
        Task<ApiResult<string>> Register(RegisterModel model);
        Task<ApiResult<bool>> Logout(string refreshToken);
        Task<ApiResult<TokenResponse>> RefreshToken(TokenRequest request);
        Task<ApiResult<string>> RequestForgotPassword(MailRequest request);
        Task<ApiResult<bool>> ResetPassword(ForgotPasswordRequest request);
        Task<ApiResult<bool>> ChangePassword(ChangePasswordRequest request, int userId);
    }
}