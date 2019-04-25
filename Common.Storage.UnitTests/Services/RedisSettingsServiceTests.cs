using Common.Services;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;

namespace Common.Storage.UnitTests.Services
{
    public class RedisSettingsServiceTests
    {
        private readonly Mock<IConfiguration> _configuration;

        public RedisSettingsServiceTests()
        {
            _configuration = new Mock<IConfiguration>();
            _configuration.SetupGet(x => x[It.IsAny<string>()]).Returns("configuration string");
        }

        [Fact]
        public void Should_get_redisconnectionstring_from_redis_settings()
        {
            //Arrange
            var Sut = new RedisSettingsService(_configuration.Object);

            //Act
            var reddisConnectionString = Sut.RedisConnectionString;

            //Assert
            reddisConnectionString.Should().Be("configuration string");
        }
    }
}
