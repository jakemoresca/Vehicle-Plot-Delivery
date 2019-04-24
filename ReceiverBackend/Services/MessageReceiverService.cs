using Common.Services;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace ReceiverBackend.Services
{
    public class MessageReceiverService : IMessageReceiverService
    {
        private readonly IQueueSettingsService _queueSettingsService;
        private readonly IMessageProcessingService _messageProcessingService;
        private readonly IConnection _connection;

        public MessageReceiverService(IQueueSettingsService queueSettingsService, IMessageProcessingService messageProcessingService, IConnection connection)
        {
            _queueSettingsService = queueSettingsService;
            _messageProcessingService = messageProcessingService;
            _connection = connection;
        }

        public void StartReceivingMessage()
        {
            var channel = _connection.CreateModel();
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
            _connection.Close();
        }

        private void AddReceivedEvent(IModel channel, EventingBasicConsumer consumer)
        {
            consumer.Received += (model, eventArguments) =>
            {
                var body = eventArguments.Body;

                _messageProcessingService.Process(body);

                channel.BasicAck(deliveryTag: eventArguments.DeliveryTag, multiple: false);
            };
        }
    }
}
