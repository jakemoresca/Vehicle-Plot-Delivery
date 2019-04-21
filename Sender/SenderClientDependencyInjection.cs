using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SenderBackend;

namespace SenderClient
{
    public static class SenderClientDependencyInjection
    {
        public static void Register(ServiceCollection serviceCollection)
        {
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
