using Common;
using Microsoft.Extensions.DependencyInjection;
using SenderBackend.Services;

namespace SenderBackend
{
    public static class SenderBackendDependencyInjection
    {
        public static void Register(ServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IVehiclePlotService, VehiclePlotService>();
            serviceCollection.AddScoped<IMessagingService, MessagingService>();
            CommonDependencyInjection.Register(serviceCollection);
        }
    }
}
