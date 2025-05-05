using StackExchange.Redis;
using System.Text.Json;

namespace MrKatsuWebAPI.Application.Redis
{
    public interface IRedisService
    {
        Task SetAsync(string key, string value, TimeSpan? expiry = null);
        Task<string?> GetAsync(string key);
        Task RemoveValue(string key);
        Task<T?> GetValue<T>(string key);
        Task SetValue<T>(string key, T value, TimeSpan? expiry = null);
        Task IncrementValue(string key);
        Task SetKeyExpire(string key, TimeSpan? expiry = null);
        Task<bool> KeyExist(string key);
    }
    public class RedisService : IRedisService
    {
        private readonly IConnectionMultiplexer _connectionMultiplexer;
        public RedisService(IConnectionMultiplexer connectionMultiplexer)
        {
            _connectionMultiplexer = connectionMultiplexer;
        }
        private IDatabase Database => _connectionMultiplexer.GetDatabase();
        public async Task SetAsync(string key, string value, TimeSpan? expiry = null)
        {
            await Database.StringSetAsync(key, value, expiry);
        }

        public async Task<string?> GetAsync(string key)
        {
            return await Database.StringGetAsync(key);
        }

        public async Task RemoveValue(string key)
        {
            await Database.KeyDeleteAsync(key);
        }
        public async Task<T?> GetValue<T>(string key)
        {
            var json = await Database.StringGetAsync(key);
            if (json.IsNullOrEmpty)
            {
                return default!;
            }
            return JsonSerializer.Deserialize<T>(json.ToString(), _jsonOptions);
        }
        public async Task SetValue<T>(string key, T value, TimeSpan? expiry = null)
        {
            await Database.StringSetAsync(key, JsonSerializer.Serialize(value, _jsonOptions), expiry);
        }
        public async Task<bool> KeyExist(string key)
        {
            return await Database.KeyExistsAsync(key);
        }

        public async Task IncrementValue(string key)
        {
            await Database.StringIncrementAsync(key);
        }

        public async Task SetKeyExpire(string key, TimeSpan? expiry = null)
        {
            await Database.KeyExpireAsync(key, expiry);
        }

        private readonly JsonSerializerOptions _jsonOptions = new()
        {
            PropertyNameCaseInsensitive = true,
            WriteIndented = false
        };
    }
}
