using Common.Models;
using QueryServiceWeb.DTOs;

namespace QueryServiceWeb.Mappers
{
    public class VehicleJourneyMapper : IVehicleJourneyMapper
    {
        public VehicleJourneyDto ToDto(VehicleJourney vehicleJourney)
        {
            return new VehicleJourneyDto
            {
                VehicleId = vehicleJourney.VehicleId,
                Latitude = vehicleJourney.Latitude,
                Longitude = vehicleJourney.Longitude,
                Timestamp = vehicleJourney.Timestamp,
                EventCode = vehicleJourney.EventCode.ToString(),
                JourneyStart = vehicleJourney.JourneyStart,
                JourneyEnd = vehicleJourney.JourneyEnd
            };
        }
    }
}
