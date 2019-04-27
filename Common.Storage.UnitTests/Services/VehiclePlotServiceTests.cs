using Common.Models;
using Common.Storage.Repositories;
using Common.Storage.Services;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Common.Storage.UnitTests.Services
{
    public class VehiclePlotServiceTests
    {
        private readonly Mock<IVehiclePlotRepository> _vehiclePlotRepository;

        public VehiclePlotServiceTests()
        {
            _vehiclePlotRepository = new Mock<IVehiclePlotRepository>();
        }

        [Theory]
        [InlineData(1, -40, 40, EventCode.IgnitionOn)]
        [InlineData(2, -30.47, 40.142, EventCode.Movement)]
        [InlineData(3, 100, 0, EventCode.IgnitionOff)]

        public async Task Should_insert_vehicle_plot(int vehicleId, double latitude, double longitude, EventCode eventCode)
        {
            //Arrange
            _vehiclePlotRepository.Setup(x => x.InsertAsync(It.IsAny<VehiclePlot>())).Returns(Task.FromResult(0));

            var Sut = new VehiclePlotService(_vehiclePlotRepository.Object);
            var vehiclePlot = new VehiclePlot(vehicleId, latitude, longitude, DateTime.UtcNow, eventCode);

            //Act
            await Sut.InsertAsync(vehiclePlot);

            //Assert
            _vehiclePlotRepository.Verify(x => x.InsertAsync(vehiclePlot));
        }
    }
}
