using StackExchange.Redis;
using System.Threading.Tasks;

namespace Common.Storage.Daos
{
    public interface IVehiclePlotDao
    {
        Task InsertAsync(VehiclePlotDto vehiclePlotDto);
        Task<RedisValue[]> FindAllVehiclePlotsAsync(int vehiclePlotId, double score);
    }
}