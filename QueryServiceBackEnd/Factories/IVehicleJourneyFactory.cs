using System.Collections.Generic;
using Common.Models;

namespace QueryServiceBackEnd.Factories
{
    public interface IVehicleJourneyFactory
    {
        List<VehicleJourney> CreateVehicleJourneys(List<VehiclePlot> vehiclePlots);
    }
}