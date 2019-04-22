using Common.Services;
using Common.Storage.Daos;
using Common.Storage.Factories;
using Common.Storage.Repositories;
using Common.Storage.Services;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace Common.Storage
{
    public static class CommonStorageDepedencyInjection
    {
        public static void Register(ServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IRedisSettingsService, RedisSettingsService>();
            serviceCollection.AddScoped<IVehiclePlotFactory, VehiclePlotFactory>();
            serviceCollection.AddScoped<IVehiclePlotRepository, VehiclePlotRepository>();
            serviceCollection.AddScoped<IVehiclePlotService, VehiclePlotService>();
            serviceCollection.AddScoped<IVehiclePlotDao, VehiclePlotDao>();

            serviceCollection.AddSingleton<IConnectionMultiplexer>(x => 
                ConnectionMultiplexer.Connect(x.GetRequiredService<IRedisSettingsService>().RedisConnectionString));

            CommonDependencyInjection.Register(serviceCollection);
        }
    }
}
