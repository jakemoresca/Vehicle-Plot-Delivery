using Common.DTOs;
using Common.Mappers;
using Common.Models;
using Common.Serializers;
using Moq;
using SenderBackend.Services;
using System;
using System.Text;
using Xunit;

namespace SenderBackEnd.UnitTests.Services
{
    public class VehiclePlotServiceTests
    {
        private readonly Mock<IMessagingService> _messagingService;
        private readonly Mock<IVehiclePlotMapper> _vehiclePlotMapper;
        private readonly Mock<IVehiclePlotSerializer> _vehiclePlotSerializer;

        public VehiclePlotServiceTests()
        {
            _messagingService = new Mock<IMessagingService>();
            _vehiclePlotMapper = new Mock<IVehiclePlotMapper>();
            _vehiclePlotSerializer = new Mock<IVehiclePlotSerializer>();
        }

        [Fact]
        public void Should_send_vehicle_plot()
        {
            //Arrange
            var vehiclePlot = new VehiclePlot(1, 0, 0, DateTime.UtcNow, EventCode.IgnitionOn);
            var messageBody = Encoding.UTF8.GetBytes("test");
            var vehiclePlotDto = new VehiclePlotDto
            {
                VehicleId = 1,
                Latitude = 0,
                Longitude = 0,
                Timestamp = DateTime.UtcNow,
                EventCode = EventCode.IgnitionOn
            };

            _vehiclePlotMapper.Setup(x => x.ToDto(It.IsAny<VehiclePlot>()))
                .Returns(vehiclePlotDto);

            _vehiclePlotSerializer.Setup(x => x.Serialize(It.IsAny<VehiclePlotDto>()))
                .Returns(messageBody);

            var Sut = new VehiclePlotService(_messagingService.Object, _vehiclePlotMapper.Object, _vehiclePlotSerializer.Object);

            //Act
            Sut.Send(vehiclePlot);

            //Assert
            _vehiclePlotMapper.Verify(x => x.ToDto(vehiclePlot));
            _vehiclePlotSerializer.Verify(x => x.Serialize(vehiclePlotDto));
            _messagingService.Verify(x => x.SendMessage(messageBody));
        }
    }
}
