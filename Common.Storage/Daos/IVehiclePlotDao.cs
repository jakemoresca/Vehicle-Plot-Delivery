using StackExchange.Redis;
using System.Threading.Tasks;

namespace Common.Storage.Daos
{
    public interface IVehiclePlotDao
    {
        Task<bool> InsertAsync(VehiclePlotDto vehiclePlotDto);
        Task<RedisValue[]> FindAllVehiclePlotsAsync(string vehiclePlotId, double score);
    }
}