namespace Common.Services
{
    public interface IQueueSettingsService
    {
        string Queue { get; }
        string RabbitMQUri { get; }
    }
}