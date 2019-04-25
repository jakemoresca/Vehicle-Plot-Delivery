using CommandLine;
using Microsoft.Extensions.DependencyInjection;
using SenderClient.Services;
using System;

namespace SenderClient
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Vehicle Plot Sender Client");
            Console.WriteLine("Press Ctrl + C to close.");

            CommandLineOptions options = null;

            Parser.Default.ParseArguments<CommandLineOptions>(args)
                .WithParsed(opts => options = opts)
                .WithNotParsed(errors => throw new InvalidOperationException(string.Join(Environment.NewLine, errors)));

            if (options == null)
            {
                throw new InvalidOperationException("unable to parse arguments");
            }

            var serviceProvider = CreateServiceProvider();
            var vehiclePlotPeriodicUpdateService = serviceProvider.GetRequiredService<IVehiclePlotPeriodicUpdateService>();

            vehiclePlotPeriodicUpdateService.Start(options.Interval, options.VehicleId);

            Console.ReadLine();
            vehiclePlotPeriodicUpdateService.Stop();

            return;
        }

        private static ServiceProvider CreateServiceProvider()
        {
            var serviceCollection = new ServiceCollection();
            SenderClientDependencyInjection.Register(serviceCollection);

            return serviceCollection.BuildServiceProvider();
        }
    }
}
