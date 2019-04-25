using Common.DTOs;
using Common.Mappers;
using Common.Models;
using Common.Serializers;
using Common.Storage.Services;
using Microsoft.Extensions.Logging;
using Moq;
using ReceiverBackend.Services;
using System;
using System.Text;
using Xunit;

namespace ReceiverBackEnd.UnitTests.Services
{
    public class MessageProcessingServiceTests
    {
        private readonly Mock<IVehiclePlotSerializer> _vehiclePlotSerializer;
        private readonly Mock<IVehiclePlotMapper> _vehiclePlotMapper;
        private readonly Mock<IVehiclePlotService> _vehiclePlotService;
        private readonly Mock<ILogger<IMessageProcessingService>> _logger;

        public MessageProcessingServiceTests()
        {
            _vehiclePlotSerializer = new Mock<IVehiclePlotSerializer>();
            _vehiclePlotMapper = new Mock<IVehiclePlotMapper>();
            _vehiclePlotService = new Mock<IVehiclePlotService>();
            _logger = new Mock<ILogger<IMessageProcessingService>>();
        }

        [Fact]
        public void Should_process_message()
        {
            //Arrange
            var messageBody = Encoding.UTF8.GetBytes("test");
            var vehiclePlot = new VehiclePlot(1, 0, 0, DateTime.UtcNow, EventCode.IgnitionOn);
            var vehiclePlotDto = new VehiclePlotDto
            {
                VehicleId = 1
            };

            _vehiclePlotSerializer.Setup(x => x.Deserialize(It.IsAny<byte[]>()))
                .Returns(vehiclePlotDto);

            _vehiclePlotMapper.Setup(x => x.ToModel(It.IsAny<VehiclePlotDto>()))
                .Returns(vehiclePlot);

            _vehiclePlotService.Setup(x => x.InsertAsync(It.IsAny<VehiclePlot>()));

            var Sut = new MessageProcessingService(_vehiclePlotSerializer.Object, _vehiclePlotMapper.Object, _vehiclePlotService.Object, _logger.Object);

            //Act
            Sut.Process(messageBody);

            //Assert
            _vehiclePlotSerializer.Verify(x => x.Deserialize(messageBody));
            _vehiclePlotMapper.Verify(x => x.ToModel(vehiclePlotDto));
            _vehiclePlotService.Verify(x => x.InsertAsync(vehiclePlot));
        }
    }
}
