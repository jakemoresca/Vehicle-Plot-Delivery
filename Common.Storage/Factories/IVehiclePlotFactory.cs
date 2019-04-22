using Common.Models;
using Common.Storage.Daos;
using StackExchange.Redis;

namespace Common.Storage.Factories
{
    public interface IVehiclePlotFactory
    {
        VehiclePlotDto ToDto(VehiclePlot vehiclePlot);
        VehiclePlot ToModel(RedisValue vehiclePlotValue);
    }
}