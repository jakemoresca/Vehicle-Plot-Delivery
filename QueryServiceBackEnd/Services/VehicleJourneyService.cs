using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Models;
using Common.Storage.Repositories;
using QueryServiceBackEnd.Factories;

namespace QueryServiceBackEnd.Services
{
    public class VehicleJourneyService : IVehicleJourneyService
    {
        private readonly IVehiclePlotRepository _vehiclePlotRepository;
        private readonly IVehicleJourneyFactory _vehicleJourneyFactory;

        public VehicleJourneyService(IVehiclePlotRepository vehiclePlotRepository, IVehicleJourneyFactory vehicleJourneyFactory)
        {
            _vehiclePlotRepository = vehiclePlotRepository;
            _vehicleJourneyFactory = vehicleJourneyFactory;
        }

        public async Task<List<VehicleJourney>> FindVehicleJourneyAsync(int vehicleId, DateTime timeStart, DateTime timeEnd)
        {
            var vehiclePlots = await _vehiclePlotRepository.FindVehiclePlotsAsync(vehicleId, timeStart.ToOADate(), timeEnd.ToOADate());
            return _vehicleJourneyFactory.CreateVehicleJourneys(vehiclePlots);
        }
    }
}
