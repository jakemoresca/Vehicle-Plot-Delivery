using Common.Services;
using Moq;
using RabbitMQ.Client;
using SenderBackend.Services;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace SenderBackEnd.UnitTests.Services
{
    public class MessagingServiceTests
    {
        private readonly Mock<IBasicProperties> _basicProperties;
        private readonly Mock<IModel> _model;
        private readonly Mock<IConnection> _connection;
        private readonly Mock<IQueueSettingsService> _queueSettingsService;

        public MessagingServiceTests()
        {
            _basicProperties = new Mock<IBasicProperties>();
            _model = new Mock<IModel>();
            _connection = new Mock<IConnection>();
            _queueSettingsService = new Mock<IQueueSettingsService>();
        }

        [Fact]
        public void Should_send_message()
        {
            //Arrange
            var basicProperties = _basicProperties.Object;

            _model.Setup(x => x.CreateBasicProperties())
                .Returns(basicProperties);

            var model = _model.Object;

            _connection.Setup(x => x.CreateModel())
                .Returns(model);

            var queue = "test";

            _queueSettingsService.SetupGet(x => x.Queue)
                .Returns(queue);

            var messageBody = Encoding.UTF8.GetBytes("test");

            var Sut = new MessagingService(_connection.Object, _queueSettingsService.Object);

            //Act
            Sut.SendMessage(messageBody);

            //Assert
            _model.Verify(x => x.QueueDeclare(queue, It.IsAny<bool>(), It.IsAny<bool>(), It.IsAny<bool>(), It.IsAny<IDictionary<string, object>>()));
        }
    }
}
