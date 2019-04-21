using Microsoft.Extensions.Configuration;

namespace Common.Services
{
    public class QueueSettingsService : IQueueSettingsService
    {
        private readonly IConfiguration _configuration;

        public QueueSettingsService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string RabbitMQUri => _configuration["rabbitMQUri"];

        public string Queue => _configuration["queue"];
    }
}
