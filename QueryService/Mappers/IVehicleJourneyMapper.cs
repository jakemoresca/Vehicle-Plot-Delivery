using Common.Models;
using QueryServiceWeb.DTOs;

namespace QueryServiceWeb.Mappers
{
    public interface IVehicleJourneyMapper
    {
        VehicleJourneyDto ToDto(VehicleJourney vehicleJourney);
    }
}