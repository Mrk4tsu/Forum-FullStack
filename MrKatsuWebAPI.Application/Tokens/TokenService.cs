using MrKatsuWebAPI.DTO.Settings;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using MrKatsuWebAPI.DataAccess.Entities;
using MrKatsuWebAPI.DTO.Authorize;
using MrKatsuWebAPI.Application.Redis;
using MrKatsuWebAPI.Helper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
namespace MrKatsuWebAPI.Application.Tokens
{
    public class TokenService
    {
        private readonly JwtSettings _jwtSettings;
        private readonly IRedisService _redisService;
        private readonly UserManager<AppUser> _userManager;
        private readonly ILogger<TokenService> _logger;
        public TokenService(IOptions<JwtSettings> jwtSettings,
            IRedisService redisService, ILogger<TokenService> logger, UserManager<AppUser> userManager)
        {
            _redisService = redisService;
            _jwtSettings = jwtSettings.Value;
            _userManager = userManager;
            _logger = logger;
        }
        public async Task<TokenResponse> GenerateJwtToken(AppUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Name, user.UserName!),
                new Claim(JwtRegisteredClaimNames.GivenName, user.Avatar),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Typ,roles.First()),
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes),
                signingCredentials: creds);
            //Log thời gian hết hạn của Token
            _logger.LogInformation($"Date time now: {DateTime.UtcNow.ToString("hh mm ss")}");
            _logger.LogInformation($"Token expires at: {token.ValidTo}");

            var refreshToken = Guid.NewGuid().ToString();

            var cacheKey = $"refresh-token:{refreshToken}";
            var cacheValue = user.Id.ToString();
            await _redisService.SetAsync(cacheKey, cacheValue, TimeSpan.FromDays(_jwtSettings.RefreshExpiryDays));

            var tokenResponse = new TokenResponse
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                RefreshToken = refreshToken,
                ExpiresAt = DateTime.UtcNow.AddDays(_jwtSettings.RefreshExpiryDays),
            };
            return tokenResponse;
        }
        public string GenerateRefreshToken() => Guid.NewGuid().ToString();
        public async Task<TokenResponse> RefreshTokenAsync(string refreshToken)
        {
            var cacheKey = $"refresh-token:{refreshToken}";
            var userId = await _redisService.GetAsync(cacheKey);
            if (string.IsNullOrEmpty(userId))
                return null;
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return null;
            await _redisService.RemoveValue(cacheKey);
            var newToken = await GenerateJwtToken(user);

            return newToken;
        }
        public async Task RemoveToken(string refreshToken)
        {
            var cacheKey = $"refresh-token:{refreshToken}";
            await _redisService.RemoveValue(cacheKey);
        }
    }
}
