using Common.Services;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using ReceiverBackend.Services;
using System;

namespace ReceiverClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = CreateServiceProvider();
            var messageReceiverService = serviceProvider.GetRequiredService<IMessageReceiverService>();

#if DEBUG
            messageReceiverService.StartReceivingMessage();
#else
            using (var service = new ReceiverService(messageReceiverService))
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
