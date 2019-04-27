using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ReceiverBackend.Services;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;

namespace ReceiverClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var isConsole = Debugger.IsAttached || args.Contains("--console");

            var serviceProvider = CreateServiceProvider();
            var messageReceiverService = serviceProvider.GetRequiredService<IMessageReceiverService>();
            var logger = serviceProvider.GetRequiredService<ILogger<ReceiverService>>();

            if (isConsole)
            {
                messageReceiverService.StartReceivingMessage();
            }
            else
            {
                using (var service = new ReceiverService(messageReceiverService, logger))
                {
                    ServiceBase.Run(service);
                }
            }
        }

        private static ServiceProvider CreateServiceProvider()
        {
            var serviceCollection = new ServiceCollection();
            ReceiverClientDependencyInjection.Register(serviceCollection);

            return serviceCollection.BuildServiceProvider();
        }
    }
}
