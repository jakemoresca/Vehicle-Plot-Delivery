using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Models;
using Common.Storage.Repositories;
using FluentAssertions;
using Moq;
using QueryServiceBackEnd.Factories;
using QueryServiceBackEnd.Services;
using Xunit;

namespace QueryServiceBackEnd.UnitTests.Services
{
    public class VehicleJourneyServiceTests
    {
        private readonly Mock<IVehiclePlotRepository> _vehiclePlotRepository;
        private readonly Mock<IVehicleJourneyFactory> _vehicleJourneyFactory;

        public VehicleJourneyServiceTests()
        {
            _vehicleJourneyFactory = new Mock<IVehicleJourneyFactory>();
            _vehiclePlotRepository = new Mock<IVehiclePlotRepository>();
        }

        [Theory]
        [InlineData(1, "2019-04-24T15:11:28Z", "2019-04-25T15:11:28Z")]
        [InlineData(2, "2019-04-24T12:00:00Z", "2019-04-25T15:11:28Z")]
        [InlineData(3, "2019-04-23T15:11:28Z", "2019-04-25T15:11:28Z")]
        public async Task Should_find_vehicle_journeys(int vehicleId, string timeStart, string timeEnd)
        {
            //Arrange
            var vehiclePlots = new List<VehiclePlot>();

            _vehiclePlotRepository.Setup(x => x.FindVehiclePlotsAsync(It.IsAny<int>(), It.IsAny<double>(), It.IsAny<double>()))
                .ReturnsAsync(vehiclePlots);

            var vehicleJourneys = new List<VehicleJourney>();

            _vehicleJourneyFactory.Setup(x => x.CreateVehicleJourneys(It.IsAny<List<VehiclePlot>>()))
                .Returns(vehicleJourneys);

            var Sut = new VehicleJourneyService(_vehiclePlotRepository.Object, _vehicleJourneyFactory.Object);

            var dateTimeStart = DateTime.Parse(timeStart);
            var dateTimeEnd = DateTime.Parse(timeEnd);

            //Act
            var actual = await Sut.FindVehicleJourneyAsync(vehicleId, dateTimeStart, dateTimeEnd);

            //Assert
            _vehiclePlotRepository.Verify(x => x.FindVehiclePlotsAsync(vehicleId, dateTimeStart.ToOADate(), dateTimeEnd.ToOADate()));
            _vehicleJourneyFactory.Verify(x => x.CreateVehicleJourneys(vehiclePlots));
            actual.Should().BeSameAs(vehicleJourneys);
        }
    }
}
