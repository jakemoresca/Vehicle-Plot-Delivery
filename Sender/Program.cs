using CommandLine;
using Common.Models;
using Microsoft.Extensions.DependencyInjection;
using SenderBackend.Services;
using System;

namespace SenderClient
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            CommandLineOptions options = null;

            Parser.Default.ParseArguments<CommandLineOptions>(args)
                .WithParsed(opts => options = opts)
                .WithNotParsed(errors => throw new InvalidOperationException(string.Join(Environment.NewLine, errors)));

            if (options == null)
            {
                throw new InvalidOperationException("unable to parse arguments");
            }

            var serviceProvider = CreateServiceProvider();

            var vehiclePlotService = serviceProvider.GetRequiredService<IVehiclePlotService>();

            var vehiclePlot = new VehiclePlot(1, 1, 1, DateTime.UtcNow, EventCode.IgnitionOn);
            vehiclePlotService.Send(vehiclePlot);
        }

        private static ServiceProvider CreateServiceProvider()
        {
            var serviceCollection = new ServiceCollection();
            SenderClientDependencyInjection.Register(serviceCollection);

            return serviceCollection.BuildServiceProvider();
        }
    }
}
