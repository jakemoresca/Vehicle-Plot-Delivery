using Common.Mappers;
using Common.Models;
using Common.Serializers;

namespace SenderBackend.Services
{
    public class VehiclePlotService : IVehiclePlotService
    {
        private readonly IMessagingService _messagingService;
        private readonly IVehiclePlotMapper _vehiclePlotMapper;
        private readonly IVehiclePlotSerializer _vehiclePlotSerializer;

        public VehiclePlotService(IMessagingService messagingService, IVehiclePlotMapper vehiclePlotMapper, IVehiclePlotSerializer vehiclePlotSerializer)
        {
            _messagingService = messagingService;
            _vehiclePlotMapper = vehiclePlotMapper;
            _vehiclePlotSerializer = vehiclePlotSerializer;
        }

        public void Send(VehiclePlot vehiclePlot)
        {
            var vehiclePlotDto = _vehiclePlotMapper.ToDto(vehiclePlot);
            var vehiclePlotMessageBody = _vehiclePlotSerializer.Serialize(vehiclePlotDto);

            _messagingService.SendMessage(vehiclePlotMessageBody);
        }
    }
}
