namespace MrKatsuWebAPI.DTO.Settings
{
    public class MailSetting
    {
        public string ApiKey { get; set; } = string.Empty;
        public string SecretKey { get; set; } = string.Empty;
        public string SenderEmail { get; set; } = string.Empty;
        public string SenderName { get; set; } = string.Empty;
    }
}
