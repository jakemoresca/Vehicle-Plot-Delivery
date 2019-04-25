using Common.Models;
using Common.Storage.Daos;
using Common.Storage.Factories;
using Common.Storage.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Common.Storage.UnitTests.Repositories
{
    public class VehiclePlotRepositoryTests
    {
        private readonly Mock<IVehiclePlotFactory> _vehiclePlotFactory;
        private readonly Mock<IVehiclePlotDao> _vehiclePlotDao;

        public VehiclePlotRepositoryTests()
        {
            _vehiclePlotFactory = new Mock<IVehiclePlotFactory>();
            _vehiclePlotDao = new Mock<IVehiclePlotDao>();
        }

        [Theory]
        [InlineData(1, -40, 40, EventCode.IgnitionOn)]
        [InlineData(2, -30.47, 40.142, EventCode.Movement)]
        [InlineData(3, 100, 0, EventCode.IgnitionOff)]
        public async Task Should_insert_vehicle_plot(int vehicleId, double latitude, double longitude, EventCode eventCode)
        {
            //Arrange
            var vehiclePlotDto = new VehiclePlotDto
            {
                Id = vehicleId
            };

            _vehiclePlotFactory.Setup(x => x.ToDto(It.IsAny<VehiclePlot>()))
                .Returns(vehiclePlotDto);

            _vehiclePlotDao.Setup(x => x.InsertAsync(It.IsAny<VehiclePlotDto>()))
                .Returns(Task.FromResult(0));

            var Sut = new VehiclePlotRepository(_vehiclePlotFactory.Object, _vehiclePlotDao.Object);
            var vehiclePlot = new VehiclePlot(vehicleId, latitude, longitude, DateTime.UtcNow, eventCode);

            //Act
            await Sut.InsertAsync(vehiclePlot);

            //Assert
            _vehiclePlotFactory.Verify(x => x.ToDto(vehiclePlot));
            _vehiclePlotDao.Verify(x => x.InsertAsync(vehiclePlotDto));
        }
    }
}
