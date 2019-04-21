using Common.Mappers;
using Common.Serializers;

namespace ReceiverBackend.Services
{
    public class MessageProcessingService : IMessageProcessingService
    {
        private readonly IVehiclePlotSerializer _vehiclePlotSerializer;
        private readonly IVehiclePlotMapper _vehiclePlotMapper;

        public MessageProcessingService(IVehiclePlotSerializer vehiclePlotSerializer, IVehiclePlotMapper vehiclePlotMapper)
        {
            _vehiclePlotSerializer = vehiclePlotSerializer;
            _vehiclePlotMapper = vehiclePlotMapper;
        }

        public void Process(byte[] messageBody)
        {
            var vehiclePlotDto = _vehiclePlotSerializer.Deserialize(messageBody);
            var vehiclePlot = _vehiclePlotMapper.ToModel(vehiclePlotDto);
        }
    }
}
