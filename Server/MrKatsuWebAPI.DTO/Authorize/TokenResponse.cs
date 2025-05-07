namespace MrKatsuWebAPI.DTO.Authorize
{
    public class TokenResponse
    {
        public string AccessToken { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
        public string ClientId { get; set; } = string.Empty;
        public DateTime ExpiresAt { get; set; }
    }
}
