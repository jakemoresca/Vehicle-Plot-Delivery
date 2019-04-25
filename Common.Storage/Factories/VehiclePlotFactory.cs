using Common.Models;
using Common.Storage.Daos;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using StackExchange.Redis;

namespace Common.Storage.Factories
{
    public class VehiclePlotFactory : IVehiclePlotFactory
    {
        private readonly JsonSerializerSettings _jsonSerializerSettings;

        public VehiclePlotFactory()
        {
            _jsonSerializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                DateTimeZoneHandling = DateTimeZoneHandling.Utc,
                DateFormatHandling = DateFormatHandling.IsoDateFormat
            };
        }

        public VehiclePlotDto ToDto(VehiclePlot vehiclePlot)
        {
            var definition = JsonConvert.SerializeObject(vehiclePlot, _jsonSerializerSettings);

            return new VehiclePlotDto
            {
                Id = vehiclePlot.VehicleId,
                Definition = definition,
                Score = vehiclePlot.Timestamp.ToOADate()
            };
        }

        public VehiclePlot ToModel(RedisValue vehiclePlotValue)
        {
            return JsonConvert.DeserializeObject<VehiclePlot>(vehiclePlotValue, _jsonSerializerSettings);
        }
    }
}
