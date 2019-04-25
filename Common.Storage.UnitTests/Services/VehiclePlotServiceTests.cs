using Common.Models;
using Common.Storage.Repositories;
using Common.Storage.Services;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
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

        [Theory]
        [InlineData(1, "2019-04-24T15:11:28Z")]
        [InlineData(2, "2019-04-24T12:00:00Z")]
        [InlineData(3, "2019-04-23T15:11:28Z")]
        public async Task Should_find_all_vehicle_plot(int vehicleId, string timestamp)
        {
            //Arrange
            var vehiclePlot = new VehiclePlot(vehicleId, 0, 0, DateTime.Parse(timestamp).ToUniversalTime(), EventCode.IgnitionOff);

            _vehiclePlotRepository.Setup(x => x.FindAllVehiclePlotsAsync(It.IsAny<int>(), It.IsAny<double>()))
                .ReturnsAsync(new List<VehiclePlot>()
                {
                    vehiclePlot
                });

            var Sut = new VehiclePlotService(_vehiclePlotRepository.Object);

            //Act
            var expected = await Sut.FindAllVehiclePlotsAsync(vehicleId, vehiclePlot.Timestamp);

            //Assert
            _vehiclePlotRepository.Verify(x => x.FindAllVehiclePlotsAsync(vehicleId, vehiclePlot.Timestamp.ToOADate()));
            expected.Should().Contain(vehiclePlot);
        }
    }
}
