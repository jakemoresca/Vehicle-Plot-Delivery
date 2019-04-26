using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Models;

namespace Common.Storage.Services
{
    public interface IVehiclePlotService
    {
        Task InsertAsync(VehiclePlot vehiclePlot);
    }
}