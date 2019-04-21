using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ReceiverBackend;

namespace ReceiverClient
{
    public class ReceiverClientDependencyInjection
    {
        public static void Register(ServiceCollection serviceCollection)
        {
            ReceiverBackEndDependencyInjection.Register(serviceCollection);

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
