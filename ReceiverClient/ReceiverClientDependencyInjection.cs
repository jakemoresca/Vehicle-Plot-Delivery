using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ReceiverBackend;

namespace ReceiverClient
{
    public class ReceiverClientDependencyInjection
    {
        public static void Register(IServiceCollection serviceCollection)
        {
            ReceiverBackEndDependencyInjection.Register(serviceCollection);

            serviceCollection.AddLogging(configure =>
            {
                configure.AddConsole()
                    .AddEventLog();
            });

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
