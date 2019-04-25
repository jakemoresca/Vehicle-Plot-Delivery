using Common.Models;
using Common.Storage.Factories;
using FluentAssertions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using Xunit;

namespace Common.Storage.UnitTests.Factories
{
    public class VehiclePlotFactoryTests
    {
        private readonly IVehiclePlotFactory Sut;
        private readonly JsonSerializerSettings _jsonSerializerSettings;

        public VehiclePlotFactoryTests()
        {
            Sut = new VehiclePlotFactory();
            _jsonSerializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                DateTimeZoneHandling = DateTimeZoneHandling.Utc,
                DateFormatHandling = DateFormatHandling.IsoDateFormat
            };
        }

        [Theory]
        [InlineData(1, -40, 40, EventCode.IgnitionOn)]
        [InlineData(2, -30.47, 40.142, EventCode.Movement)]
        [InlineData(3, 100, 0, EventCode.IgnitionOff)]
        public void Should_convert_vehicle_plot_to_dto(int vehicleId, double latitude, double longitude, EventCode eventCode)
        {
            //Arrange
            var vehiclePlot = new VehiclePlot(vehicleId, latitude, longitude, DateTime.UtcNow, eventCode);
            var expectedDefinition = JsonConvert.SerializeObject(vehiclePlot, _jsonSerializerSettings);

            //Act
            var vehiclePlotDto = Sut.ToDto(vehiclePlot);

            //Assert
            vehiclePlotDto.Id.Should().Be(vehiclePlot.VehicleId);
            vehiclePlotDto.Score.Should().Be(vehiclePlot.Timestamp.ToOADate());
            vehiclePlotDto.Definition.Should().Be(expectedDefinition);
        }

        [Theory]
        [InlineData(1, -40, 40, EventCode.IgnitionOn, "2019-04-24T15:11:28Z")]
        [InlineData(2, -30.47, 40.142, EventCode.Movement, "2019-04-24T12:00:00Z")]
        [InlineData(3, 100, 0, EventCode.IgnitionOff, "2019-04-23T04:11:28Z")]
        public void Should_convert_redis_value_to_vehicle_plot(int vehicleId, double latitude, double longitude, EventCode eventCode, string timestamp)
        {
            //Arrange
            var dateTimestamp = DateTime.Parse(timestamp).ToUniversalTime();
            var redisValue = $"{{\"vehicleId\":{vehicleId},\"latitude\":{latitude},\"longitude\":{longitude},\"timestamp\":\"{dateTimestamp}\",\"eventCode\":{(int)eventCode}}}";

            //Act
            var vehiclePlot = Sut.ToModel(redisValue);

            //Assert
            vehiclePlot.VehicleId.Should().Be(vehicleId);
            vehiclePlot.Timestamp.Should().Be(dateTimestamp);
            vehiclePlot.Latitude.Should().Be(latitude);
            vehiclePlot.Longitude.Should().Be(longitude);
            vehiclePlot.EventCode.Should().Be(eventCode);
        }
    }
}
