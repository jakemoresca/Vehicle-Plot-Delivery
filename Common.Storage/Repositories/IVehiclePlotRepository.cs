using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Models;

namespace Common.Storage.Repositories
{
    public interface IVehiclePlotRepository
    {
        Task InsertAsync(VehiclePlot vehiclePlot);
        Task<List<VehiclePlot>> FindAllVehiclePlotsAsync(int id, double score);
    }
}