using Common.Factories;
using Common.Mappers;
using Common.Serializers;
using Common.Services;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;

namespace Common
{
    public static class CommonDependencyInjection
    {
        public static void Register(ServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IPlotConnectionFactory, PlotConnectionFactory>();
            serviceCollection.AddScoped<IQueueSettingsService, QueueSettingsService>();
            serviceCollection.AddScoped<IVehiclePlotMapper, VehiclePlotMapper>();
            serviceCollection.AddScoped<IVehiclePlotSerializer, VehiclePlotSerializer>();

            serviceCollection.AddSingleton<IConnection>(x =>
            {
                var queueSettingService = x.GetRequiredService<IQueueSettingsService>();
                var plotConnectionFactory = x.GetRequiredService<IPlotConnectionFactory>();

                return plotConnectionFactory.GetOrCreate(queueSettingService.RabbitMQUri);
            });
        }
    }
}
