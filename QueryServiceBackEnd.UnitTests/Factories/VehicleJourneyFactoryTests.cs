using System;
using System.Collections.Generic;
using Common.Models;
using FluentAssertions;
using QueryServiceBackEnd.Factories;
using Xunit;

namespace QueryServiceBackEnd.UnitTests.Factories
{
    public class VehicleJourneyFactoryTests
    {
        [Fact]
        public void Should_create_vehicle_journey_with_valid_journey_start()
        {
            //Arrange
            var vehiclePlots = new List<VehiclePlot>()
            {
                new VehiclePlot(1, 1, 1, DateTime.UtcNow, EventCode.IgnitionOn),
                new VehiclePlot(1, 1, 1, DateTime.UtcNow, EventCode.Movement),
                new VehiclePlot(1, 1, 1, DateTime.UtcNow, EventCode.Movement),
                new VehiclePlot(1, 1, 1, DateTime.UtcNow, EventCode.IgnitionOff),
                new VehiclePlot(1, 1, 1, DateTime.UtcNow, EventCode.IgnitionOn),
                new VehiclePlot(1, 1, 1, DateTime.UtcNow, EventCode.IgnitionOff),
            };

            var Sut = new VehicleJourneyFactory();

            //Act
            var expected = Sut.CreateVehicleJourneys(vehiclePlots);

            //Assert
            expected[0].JourneyStart.Value.Should().Be(expected[0].Timestamp);
            expected[1].JourneyStart.Value.Should().Be(expected[0].Timestamp);
            expected[2].JourneyStart.Value.Should().Be(expected[0].Timestamp);
            expected[3].JourneyStart.HasValue.Should().BeFalse();
            expected[4].JourneyStart.Value.Should().Be(expected[4].Timestamp);
            expected[5].JourneyStart.HasValue.Should().BeFalse();
        }

        [Fact]
        public void Should_create_vehicle_journey_with_valid_journey_end()
        {
            //Arrange
            var vehiclePlots = new List<VehiclePlot>()
            {
                new VehiclePlot(1, 1, 1, DateTime.UtcNow, EventCode.IgnitionOn),
                new VehiclePlot(1, 1, 1, DateTime.UtcNow, EventCode.Movement),
                new VehiclePlot(1, 1, 1, DateTime.UtcNow, EventCode.Movement),
                new VehiclePlot(1, 1, 1, DateTime.UtcNow, EventCode.IgnitionOff),
                new VehiclePlot(1, 1, 1, DateTime.UtcNow, EventCode.IgnitionOn),
                new VehiclePlot(1, 1, 1, DateTime.UtcNow, EventCode.IgnitionOff),
            };

            var Sut = new VehicleJourneyFactory();

            //Act
            var expected = Sut.CreateVehicleJourneys(vehiclePlots);

            //Assert
            expected[0].JourneyEnd.HasValue.Should().BeFalse();
            expected[1].JourneyEnd.HasValue.Should().BeFalse();
            expected[2].JourneyEnd.HasValue.Should().BeFalse();
            expected[3].JourneyEnd.Value.Should().Be(expected[3].Timestamp);
            expected[4].JourneyEnd.HasValue.Should().BeFalse();
            expected[5].JourneyEnd.Value.Should().Be(expected[5].Timestamp);
        }
    }
}
