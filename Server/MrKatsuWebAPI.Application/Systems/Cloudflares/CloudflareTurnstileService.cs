using Microsoft.Extensions.Configuration;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MrKatsuWebAPI.Application.Systems.Cloudflares
{
    public interface ICloudflareTurnstileService
    {
        Task<bool> VerifyTokenAsync(string token);
    }
    public class CloudflareTurnstileService : ICloudflareTurnstileService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public CloudflareTurnstileService(
            IHttpClientFactory httpClientFactory,
            IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }
        public async Task<bool> VerifyTokenAsync(string token)
        {
            if (string.IsNullOrEmpty(token))
                return false;
            var secretKey = _configuration["CloudflareTurnstile:SecretKey"];
            var remoteIp = ""; // Bạn có thể thêm IP nếu cần

            var formData = new Dictionary<string, string>
            {
                { "secret", secretKey },
                { "response", token },
                { "remoteip", remoteIp }
            };
            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.PostAsync(
                "https://challenges.cloudflare.com/turnstile/v0/siteverify",
                new FormUrlEncodedContent(formData));
            if (!response.IsSuccessStatusCode)
                return false;

            var responseBody = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<TurnstileVerifyResponse>(responseBody);

            return result?.Success ?? false;
        }
        private class TurnstileVerifyResponse
        {
            [JsonPropertyName("success")]
            public bool Success { get; set; }
            [JsonPropertyName("error-codes")]
            public List<string> ErrorCodes { get; set; }
        }
    }
}
