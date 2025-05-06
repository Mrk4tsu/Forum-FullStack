namespace MrKatsuWebAPI.DTO.Authorize
{
    public class RegisterModel
    {
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string ConfirmPassword { get; set; } = string.Empty;
        public string CaptchaToken { get; set; } = string.Empty;
        public int AvatarIndex { get; set; }
    }
}
