using Common.Factories;
using Common.Services;
using RabbitMQ.Client;

namespace SenderBackend.Services
{
    public class MessagingService : IMessagingService
    {
        private readonly IPlotConnectionFactory _plotConnectionFactory;
        private readonly IQueueSettingsService _queueSettingsService;

        public MessagingService(IPlotConnectionFactory plotConnectionFactory, IQueueSettingsService queueSettingsService)
        {
            _plotConnectionFactory = plotConnectionFactory;
            _queueSettingsService = queueSettingsService;
        }

        public void SendMessage(byte[] messageBody)
        {
            using (var connection = _plotConnectionFactory.GetOrCreate(_queueSettingsService.RabbitMQUri))
            using (var channel = connection.CreateModel())
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