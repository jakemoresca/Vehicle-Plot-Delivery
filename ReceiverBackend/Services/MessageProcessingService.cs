using Common.Mappers;
using Common.Serializers;
using Common.Storage.Services;
using Microsoft.Extensions.Logging;

namespace ReceiverBackend.Services
{
    public class MessageProcessingService : IMessageProcessingService
    {
        private readonly IVehiclePlotSerializer _vehiclePlotSerializer;
        private readonly IVehiclePlotMapper _vehiclePlotMapper;
        private readonly IVehiclePlotService _vehiclePlotService;
        private readonly ILogger<IMessageProcessingService> _logger;

        public MessageProcessingService(IVehiclePlotSerializer vehiclePlotSerializer, IVehiclePlotMapper vehiclePlotMapper, IVehiclePlotService vehiclePlotService,
            ILogger<IMessageProcessingService> logger)
        {
            _vehiclePlotSerializer = vehiclePlotSerializer;
            _vehiclePlotMapper = vehiclePlotMapper;
            _vehiclePlotService = vehiclePlotService;
            _logger = logger;
        }

        public void Process(byte[] messageBody)
        {
            var vehiclePlotDto = _vehiclePlotSerializer.Deserialize(messageBody);
            var vehiclePlot = _vehiclePlotMapper.ToModel(vehiclePlotDto);

            _logger.LogInformation($"Processing vehicle plot: {{ vehicleId: {vehiclePlot.VehicleId}, longitude: {vehiclePlot.Longitude}, " +
                    $"latitude: {vehiclePlot.Latitute}, timestamp: {vehiclePlot.Timestamp}, eventCode: {vehiclePlot.EventCode.ToString()} }}");

            _vehiclePlotService.InsertAsync(vehiclePlot);
        }
    }
}
