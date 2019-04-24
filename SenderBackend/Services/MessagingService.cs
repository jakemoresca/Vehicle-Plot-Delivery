using Common.Services;
using RabbitMQ.Client;

namespace SenderBackend.Services
{
    public class MessagingService : IMessagingService
    {
        private readonly IConnection _connection;
        private readonly IQueueSettingsService _queueSettingsService;

        public MessagingService(IConnection connection, IQueueSettingsService queueSettingsService)
        {
            _connection = connection;
            _queueSettingsService = queueSettingsService;
        }

        public void SendMessage(byte[] messageBody)
        {
            using (var channel = _connection.CreateModel())
            {
                channel.QueueDeclare(queue: _queueSettingsService.Queue,
                                     durable: true,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var properties = channel.CreateBasicProperties();
                properties.Persistent = true;

                channel.BasicPublish("", _queueSettingsService.Queue, properties, messageBody);
            }
        }
    }
}