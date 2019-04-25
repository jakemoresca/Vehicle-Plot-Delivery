using Common.Services;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;

namespace Common.UnitTests.Services
{
    public class QueueSettingServiceTests
    {
        private readonly Mock<IConfiguration> _configuration;

        public QueueSettingServiceTests()
        {
            _configuration = new Mock<IConfiguration>();
            _configuration.SetupGet(x => x[It.IsAny<string>()]).Returns("configuration string");
        }

        [Fact]
        public void Should_get_rabbitmquri_from_queue_settings()
        {
            //Arrange
            var Sut = new QueueSettingsService(_configuration.Object);

            //Act
            var rabbitmquri = Sut.RabbitMQUri;

            //Assert
            rabbitmquri.Should().Be("configuration string");
        }

        [Fact]
        public void Should_get_queue_from_queue_settings()
        {
            //Arrange
            var Sut = new QueueSettingsService(_configuration.Object);

            //Act
            var queue = Sut.Queue;

            //Assert
            queue.Should().Be("configuration string");
        }
    }
}