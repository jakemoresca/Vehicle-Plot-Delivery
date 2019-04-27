using System;

namespace QueryServiceWeb.DTOs
{
    public class VehicleJourneyDto
    {
        public int VehicleId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime Timestamp { get; set; }
        public string EventCode { get; set; }
        public DateTime? JourneyStart { get; set; }
        public DateTime? JourneyEnd { get; set; }
    }
}
