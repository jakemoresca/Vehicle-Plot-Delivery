using Common.Models;
using Microsoft.Extensions.Logging;
using SenderBackend.Services;
using SenderClient.Helpers;
using System;
using System.Threading;

namespace SenderClient.Services
{
    public class VehiclePlotPeriodicUpdateService : IVehiclePlotPeriodicUpdateService
    {
        private readonly IVehiclePlotService _vehiclePlotService;
        private readonly ILogger<IVehiclePlotPeriodicUpdateService> _logger;
        private Timer _timer;
        private int _interval;
        private VehiclePlot _lastVehiclePlot;

        public VehiclePlotPeriodicUpdateService(IVehiclePlotService vehiclePlotService, ILogger<IVehiclePlotPeriodicUpdateService> logger)
        {
            _vehiclePlotService = vehiclePlotService;
            _logger = logger;
        }

        public void Start(int interval, int vehicleId)
        {
            _interval = interval;
            _lastVehiclePlot = new VehiclePlot(vehicleId, 0, 0, DateTime.UtcNow, EventCode.IgnitionOff);

            _logger.LogInformation("Starting periodic send of vehicle plot.");
            _timer = new Timer(Tick, null, _interval, Timeout.Infinite);
        }

        private void Tick(object state)
        {
            try
            {
                var vehiclePlot = VehiclePlotHelper.GetNextVehiclePlot(_lastVehiclePlot);

                _logger.LogInformation($"Sending vehicle plot: {{ vehicleId: {vehiclePlot.VehicleId}, longitude: {vehiclePlot.Longitude}, " +
                    $"latitude: {vehiclePlot.Latitute}, timestamp: {vehiclePlot.Timestamp}, eventCode: {vehiclePlot.EventCode.ToString()} }}");

                _vehiclePlotService.Send(vehiclePlot);
                _lastVehiclePlot = vehiclePlot;
            }
            finally
            {
                _timer?.Change(_interval, Timeout.Infinite);
            }
        }

        public void Stop()
        {
            _logger.LogInformation("Stopping periodic send of vehicle plot.");
            _timer.Dispose();
        }
    }
}
