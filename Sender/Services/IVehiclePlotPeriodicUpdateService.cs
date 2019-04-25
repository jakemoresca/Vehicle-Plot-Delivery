namespace SenderClient.Services
{
    public interface IVehiclePlotPeriodicUpdateService
    {
        void Start(int interval, int vehicleId);
        void Stop();
    }
}