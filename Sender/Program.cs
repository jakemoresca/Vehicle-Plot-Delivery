using Common.Models;
using Microsoft.Extensions.DependencyInjection;
using SenderBackend.Services;
using System;

namespace SenderClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = CreateServiceProvider();

            var vehiclePlotService = serviceProvider.GetRequiredService<IVehiclePlotService>();

            var vehiclePlot = new VehiclePlot(1, 1, 1, DateTime.UtcNow, EventCode.IgnitionOn);
            vehiclePlotService.Send(vehiclePlot);

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }

        private static ServiceProvider CreateServiceProvider()
        {
            var serviceCollection = new ServiceCollection();
            SenderClientDependencyInjection.Register(serviceCollection);

            return serviceCollection.BuildServiceProvider();
        }
    }
}
