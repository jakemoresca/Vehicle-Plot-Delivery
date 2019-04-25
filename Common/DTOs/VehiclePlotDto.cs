using Common.Models;
using System;

namespace Common.DTOs
{
    [Serializable]
    public class VehiclePlotDto
    {
        public int VehicleId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime Timestamp { get; set; }
        public EventCode EventCode { get; set; }
    }
}
