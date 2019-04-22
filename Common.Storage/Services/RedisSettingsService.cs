using Microsoft.Extensions.Configuration;

namespace Common.Services
{
    public class RedisSettingsService : IRedisSettingsService
    {
        private readonly IConfiguration _configuration;

        public RedisSettingsService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string RedisConnectionString => _configuration["redisConnectionString"];
    }
}
