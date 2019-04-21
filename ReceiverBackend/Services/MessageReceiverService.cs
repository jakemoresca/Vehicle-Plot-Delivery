using Common.Factories;
using Common.Services;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace ReceiverBackend.Services
{
    public class MessageReceiverService : IMessageReceiverService
    {
        private readonly IPlotConnectionFactory _plotConnectionFactory;
        private readonly IQueueSettingsService _queueSettingsService;
        private readonly IMessageProcessingService _messageProcessingService;

        public MessageReceiverService(IPlotConnectionFactory plotConnectionFactory, IQueueSettingsService queueSettingsService, IMessageProcessingService messageProcessingService)
        {
            _plotConnectionFactory = plotConnectionFactory;
            _queueSettingsService = queueSettingsService;
            _messageProcessingService = messageProcessingService;
        }

        public void StartReceivingMessage()
        {
            var connection = _plotConnectionFactory.GetOrCreate(_queueSettingsService.RabbitMQUri);
            var channel = connection.CreateModel();

            channel.QueueDeclare(queue: _queueSettingsService.Queue,
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

            var consumer = new EventingBasicConsumer(channel);
            AddReceivedEvent(channel, consumer);

            channel.BasicConsume(queue: _queueSettingsService.Queue,
                                 autoAck: false,
                                 consumer: consumer);
        }

        public void StopReceivingMessage()
        {
            var connection = _plotConnectionFactory.GetOrCreate(_queueSettingsService.RabbitMQUri);
            connection.Close();
        }

        private void AddReceivedEvent(IModel channel, EventingBasicConsumer consumer)
        {
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body;

                _messageProcessingService.Process(body);

                channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
            };
        }
    }
}
