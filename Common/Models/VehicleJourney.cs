using System;

namespace Common.Models
{
    public class VehicleJourney : VehiclePlot
    {
        public VehicleJourney(int vehicleId, double latitude, double longitude, DateTime timestamp, EventCode eventCode, DateTime? journeyStart, DateTime? journeyEnd) : base(vehicleId, latitude, longitude, timestamp, eventCode)
        {
            JourneyStart = journeyStart;
            JourneyEnd = journeyEnd;
        }

        public DateTime? JourneyStart { get; }
        public DateTime? JourneyEnd { get; }
    }
}
