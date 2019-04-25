using System;

namespace Common.Models
{
    public class VehiclePlot
    {
        public VehiclePlot(int vehicleId, double latitude, double longitude, DateTime timestamp, EventCode eventCode)
        {
            VehicleId = vehicleId;
            Latitude = latitude;
            Longitude = longitude;
            Timestamp = timestamp;
            EventCode = eventCode;
        }

        public int VehicleId { get; }
        public double Latitude { get; }
        public double Longitude { get; }
        public DateTime Timestamp { get; }
        public EventCode EventCode { get; }
    }
}
