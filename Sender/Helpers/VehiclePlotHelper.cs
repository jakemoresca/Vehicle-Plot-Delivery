using Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SenderClient.Helpers
{
    public static class VehiclePlotHelper
    {
        const double maxValue = 90;
        const double minValue = -90;

        public static VehiclePlot GetNextVehiclePlot(VehiclePlot currentVehiclePlot)
        {
            var latitude = GetRandomDouble();
            var longitude = GetRandomDouble();
            var eventCode = GetRandomEventCode(currentVehiclePlot.EventCode);

            return new VehiclePlot(currentVehiclePlot.VehicleId, latitude, longitude, DateTime.UtcNow, eventCode);
        }

        private static double GetRandomDouble()
        {
            var random = new Random();
            return random.NextDouble() * (maxValue - minValue) + minValue;
        }

        private static EventCode GetRandomEventCode(EventCode currentEventCode)
        {
            var allowedEventCodes = new List<EventCode>();

            switch (currentEventCode)
            {
                case EventCode.IgnitionOff:
                    return EventCode.IgnitionOn;
                case EventCode.IgnitionOn:
                    allowedEventCodes.Add(EventCode.IgnitionOff);
                    allowedEventCodes.Add(EventCode.Movement);
                    break;
                case EventCode.Movement:
                    allowedEventCodes.Add(EventCode.IgnitionOff);
                    allowedEventCodes.Add(EventCode.Movement);
                    break;
            }

            var random = new Random();
            return allowedEventCodes[random.Next(allowedEventCodes.Count)];
        }
    }
}
