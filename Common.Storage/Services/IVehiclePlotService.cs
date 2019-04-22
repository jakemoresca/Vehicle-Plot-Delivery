using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Models;

namespace Common.Storage.Services
{
    public interface IVehiclePlotService
    {
        Task<bool> InsertAsync(VehiclePlot vehiclePlot);
        Task<List<VehiclePlot>> FindAllVehiclePlotsAsync(int vehicleId, DateTime timestamp);
    }
}