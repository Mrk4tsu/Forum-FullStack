using Newtonsoft.Json.Linq;

namespace MrKatsuWebAPI.Application.Mail
{
    public interface IMailService
    {
        Task<bool> SendMail(string toMail, string subject, string templateId, JObject variables);
        Task<bool> SendMail(string toMail, string subject, string templateId, Dictionary<string, object> variables);
        Task<bool> SendMail(string toEmail, string subject, string body);
        Task<bool> IsJustSendMail(int userId);
    }
}
