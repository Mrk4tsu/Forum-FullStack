namespace MrKatsuWebAPI.DTO.Authorize
{
    public class TokenRequest
    {
        public string RefreshToken { get; set; }
        public int UserId { get; set; }
        public string ClientId { get; set; }
    }
}
