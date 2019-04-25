using Common.DTOs;
using Common.Mappers;
using Common.Models;
using FluentAssertions;
using System;
using Xunit;

namespace Common.UnitTests.Mappers
{
    public class VehiclePlotMapperTests
    {
        private readonly IVehiclePlotMapper Sut;

        public VehiclePlotMapperTests()
        {
            Sut = new VehiclePlotMapper();
        }

        [Theory]
        [InlineData(1, -40, 40, EventCode.IgnitionOn)]
        [InlineData(2, -30.47, 40.142, EventCode.Movement)]
        [InlineData(3, 100, 0, EventCode.IgnitionOff)]
        public void Should_map_vehicle_plot_to_dto(int vehicleId, double latitude, double longitude, EventCode eventCode)
        {
            //Arrange
            var vehiclePlot = new VehiclePlot(vehicleId, latitude, longitude, DateTime.UtcNow, eventCode);

            //Act
            var vehiclePlotDto = Sut.ToDto(vehiclePlot);

            //Assert
            vehiclePlotDto.VehicleId.Should().Be(vehiclePlot.VehicleId);
            vehiclePlotDto.Latitude.Should().Be(vehiclePlot.Latitude);
            vehiclePlotDto.Longitude.Should().Be(vehiclePlot.Longitude);
            vehiclePlotDto.Timestamp.Should().Be(vehiclePlot.Timestamp);
            vehiclePlotDto.EventCode.Should().Be(vehiclePlot.EventCode);
        }

        [Theory]
        [InlineData(1, -40, 40, EventCode.IgnitionOn)]
        [InlineData(2, -30.47, 40.142, EventCode.Movement)]
        [InlineData(3, 100, 0, EventCode.IgnitionOff)]
        public void Should_map_dto_to_vehicle_plot(int vehicleId, double latitude, double longitude, EventCode eventCode)
        {
            //Arrange
            var vehiclePlotDto = new VehiclePlotDto
            {
                VehicleId = vehicleId,
                Latitude = latitude,
                Longitude = longitude,
                Timestamp = DateTime.UtcNow,
                EventCode = eventCode
            };

            //Act
            var vehiclePlot = Sut.ToModel(vehiclePlotDto);

            //Assert
            vehiclePlot.VehicleId.Should().Be(vehiclePlotDto.VehicleId);
            vehiclePlot.Latitude.Should().Be(vehiclePlotDto.Latitude);
            vehiclePlot.Longitude.Should().Be(vehiclePlotDto.Longitude);
            vehiclePlot.Timestamp.Should().Be(vehiclePlotDto.Timestamp);
            vehiclePlot.EventCode.Should().Be(vehiclePlotDto.EventCode);
        }
    }
}
