using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ReceiverBackend.Services;
using System.ServiceProcess;

namespace ReceiverClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = CreateServiceProvider();
            var messageReceiverService = serviceProvider.GetRequiredService<IMessageReceiverService>();
            var logger = serviceProvider.GetRequiredService<ILogger<ReceiverService>>();

#if DEBUG
            messageReceiverService.StartReceivingMessage();
#else
            using (var service = new ReceiverService(messageReceiverService, logger))
            {
                ServiceBase.Run(service);
            }
#endif
        }

        private static ServiceProvider CreateServiceProvider()
        {
            var serviceCollection = new ServiceCollection();
            ReceiverClientDependencyInjection.Register(serviceCollection);

            return serviceCollection.BuildServiceProvider();
        }
    }
}
