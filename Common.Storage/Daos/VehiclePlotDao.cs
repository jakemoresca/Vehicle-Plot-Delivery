using StackExchange.Redis;
using System.Threading.Tasks;

namespace Common.Storage.Daos
{
    public class VehiclePlotDao : IVehiclePlotDao
    {
        private readonly IDatabase _database;

        public VehiclePlotDao(IConnectionMultiplexer connectionMultiplexer)
        {
            _database = connectionMultiplexer.GetDatabase();
        }

        public async Task<bool> InsertAsync(VehiclePlotDto vehiclePlotDto)
        {
            var vehiclePlotKey = $"vehicle-plot-delivery:vehicle:{vehiclePlotDto.Id}";
            return await _database.SortedSetAddAsync(vehiclePlotKey, vehiclePlotDto.Definition, vehiclePlotDto.Score);
        }

        public async Task<RedisValue[]> FindAllVehiclePlotsAsync(string vehiclePlotId, double score)
        {
            var vehiclePlotKey = $"vehicle-plot-delivery:vehicle:{vehiclePlotId}";
            return await _database.SortedSetRangeByScoreAsync(vehiclePlotKey, score);
        }
    }
}
