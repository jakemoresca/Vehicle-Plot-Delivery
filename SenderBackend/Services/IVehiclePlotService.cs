using Common.Models;

namespace SenderBackend.Services
{
    public interface IVehiclePlotService
    {
        void Send(VehiclePlot vehiclePlot);
    }
}