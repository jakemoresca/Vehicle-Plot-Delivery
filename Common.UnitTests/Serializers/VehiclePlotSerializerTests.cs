using Common.DTOs;
using Common.Models;
using Common.Serializers;
using FluentAssertions;
using System;
using Xunit;

namespace Common.UnitTests.Serializers
{
    public class VehiclePlotSerializerTests
    {
        private readonly IVehiclePlotSerializer Sut;

        public VehiclePlotSerializerTests()
        {
            Sut = new VehiclePlotSerializer();
        }

        [Theory]
        [InlineData(1, -40, 40, EventCode.IgnitionOn)]
        [InlineData(2, -30.47, 40.142, EventCode.Movement)]
        [InlineData(3, 100, 0, EventCode.IgnitionOff)]
        public void Should_serialize_vehicle_plot_dto_to_byte_and_convert_back(int vehicleId, double latitude, double longitude, EventCode eventCode)
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
            var actualSerialized = Sut.Serialize(vehiclePlotDto);
            var actualDeserialized = Sut.Deserialize(actualSerialized);

            //Assert
            actualDeserialized.VehicleId.Should().Be(vehiclePlotDto.VehicleId);
            actualDeserialized.Latitude.Should().Be(vehiclePlotDto.Latitude);
            actualDeserialized.Longitude.Should().Be(vehiclePlotDto.Longitude);
            actualDeserialized.Timestamp.Should().Be(vehiclePlotDto.Timestamp);
            actualDeserialized.EventCode.Should().Be(vehiclePlotDto.EventCode);
        }
    }
}
