using Common.DTOs;
using Common.Models;

namespace Common.Mappers
{
    public interface IVehiclePlotMapper
    {
        VehiclePlotDto ToDto(VehiclePlot vehiclePlot);
        VehiclePlot ToModel(VehiclePlotDto vehiclePlotDto);
    }
}