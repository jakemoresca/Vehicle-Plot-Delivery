using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SenderBackend;
using SenderClient.Services;

namespace SenderClient
{
    public static class SenderClientDependencyInjection
    {
        public static void Register(ServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IVehiclePlotPeriodicUpdateService, VehiclePlotPeriodicUpdateService>();
            serviceCollection.AddLogging(x => x.AddConsole());

            SenderBackendDependencyInjection.Register(serviceCollection);

            var config = CreateConfigurationBuilder();
            serviceCollection.AddSingleton(typeof(IConfiguration), config);
        }

        private static IConfiguration CreateConfigurationBuilder()
        {
            return new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true)
                .Build();
        }
    }
}
