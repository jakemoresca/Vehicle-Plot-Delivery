using Common.DTOs;
using Common.Models;

namespace Common.Mappers
{
    public class VehiclePlotMapper : IVehiclePlotMapper
    {
        public VehiclePlotDto ToDto(VehiclePlot vehiclePlot)
        {
            return new VehiclePlotDto
            {
                VehicleId = vehiclePlot.VehicleId,
                Latitute = vehiclePlot.Latitute,
                Longitude = vehiclePlot.Longitude,
                Timestamp = vehiclePlot.Timestamp,
                EventCode = vehiclePlot.EventCode
            };
        }

        public VehiclePlot ToModel(VehiclePlotDto vehiclePlotDto)
        {
            return new VehiclePlot(vehiclePlotDto.VehicleId, vehiclePlotDto.Latitute, vehiclePlotDto.Longitude, vehiclePlotDto.Timestamp, vehiclePlotDto.EventCode);
        }
    }
}
