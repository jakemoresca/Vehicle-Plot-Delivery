namespace Common.Services
{
    public interface IRedisSettingsService
    {
        string RedisConnectionString { get; }
    }
}