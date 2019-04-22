using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Models;

namespace Common.Storage.Repositories
{
    public interface IVehiclePlotRepository
    {
        Task<bool> InsertAsync(VehiclePlot vehiclePlot);
        Task<List<VehiclePlot>> FindAllVehiclePlotsAsync(string id, double score);
    }
}