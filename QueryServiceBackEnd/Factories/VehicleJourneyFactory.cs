using System;
using System.Collections.Generic;
using Common.Models;

namespace QueryServiceBackEnd.Factories
{
    public class VehicleJourneyFactory : IVehicleJourneyFactory
    {
        public List<VehicleJourney> CreateVehicleJourneys(List<VehiclePlot> vehiclePlots)
        {
            DateTime? lastStartTime = null;
            DateTime? lastEndTime = null;

            var vehicleJourneys = new List<VehicleJourney>();

            foreach (var vehiclePlot in vehiclePlots)
            {
                switch (vehiclePlot.EventCode)
                {
                    case EventCode.IgnitionOn:
                        lastStartTime = vehiclePlot.Timestamp;
                        lastEndTime = null;
                        break;
                    case EventCode.IgnitionOff:
                        lastStartTime = null;
                        lastEndTime = vehiclePlot.Timestamp;
                        break;
                }

                var vehicleJourney = CreateVehicleJourney(vehiclePlot, lastStartTime, lastEndTime);
                vehicleJourneys.Add(vehicleJourney);
            }

            return vehicleJourneys;
        }

        private VehicleJourney CreateVehicleJourney(VehiclePlot vehiclePlot, DateTime? startTime, DateTime? endTime)
        {
            return new VehicleJourney(vehiclePlot.VehicleId, vehiclePlot.Latitude, vehiclePlot.Longitude, vehiclePlot.Timestamp, vehiclePlot.EventCode, startTime, endTime);
        }
    }
}
