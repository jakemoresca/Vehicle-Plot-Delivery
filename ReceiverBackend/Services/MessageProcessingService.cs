using Common.Mappers;
using Common.Serializers;
using Common.Storage.Services;

namespace ReceiverBackend.Services
{
    public class MessageProcessingService : IMessageProcessingService
    {
        private readonly IVehiclePlotSerializer _vehiclePlotSerializer;
        private readonly IVehiclePlotMapper _vehiclePlotMapper;
        private readonly IVehiclePlotService _vehiclePlotService;

        public MessageProcessingService(IVehiclePlotSerializer vehiclePlotSerializer, IVehiclePlotMapper vehiclePlotMapper, IVehiclePlotService vehiclePlotService)
        {
            _vehiclePlotSerializer = vehiclePlotSerializer;
            _vehiclePlotMapper = vehiclePlotMapper;
            _vehiclePlotService = vehiclePlotService;
        }

        public void Process(byte[] messageBody)
        {
            var vehiclePlotDto = _vehiclePlotSerializer.Deserialize(messageBody);
            var vehiclePlot = _vehiclePlotMapper.ToModel(vehiclePlotDto);
            _vehiclePlotService.InsertAsync(vehiclePlot);
        }
    }
}
