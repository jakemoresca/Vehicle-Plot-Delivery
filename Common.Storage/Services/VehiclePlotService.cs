using Common.Models;
using Common.Storage.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Common.Storage.Services
{
    public class VehiclePlotService : IVehiclePlotService
    {
        private readonly IVehiclePlotRepository _vehiclePlotRepository;

        public VehiclePlotService(IVehiclePlotRepository vehiclePlotRepository)
        {
            _vehiclePlotRepository = vehiclePlotRepository;
        }

        public async Task<bool> InsertAsync(VehiclePlot vehiclePlot)
        {
            return await _vehiclePlotRepository.InsertAsync(vehiclePlot);
        }

        public async Task<List<VehiclePlot>> FindAllVehiclePlotsAsync(int vehicleId, DateTime timestamp)
        {
            return await _vehiclePlotRepository.FindAllVehiclePlotsAsync(vehicleId.ToString(), timestamp.ToOADate());
        }
    }
}
