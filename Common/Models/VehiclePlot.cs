using System;

namespace Common.Models
{
    public class VehiclePlot
    {
        public VehiclePlot(int vehicleId, double latitude, double longitude, DateTime timestamp, EventCode eventCode)
        {
            VehicleId = vehicleId;
            Latitute = latitude;
            Longitude = longitude;
            Timestamp = timestamp;
            EventCode = eventCode;
        }

        public int VehicleId { get; }
        public double Latitute { get; }
        public double Longitude { get; }
        public DateTime Timestamp { get; }
        public EventCode EventCode { get; }
    }
}
