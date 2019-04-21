using Common;
using Microsoft.Extensions.DependencyInjection;
using ReceiverBackend.Services;

namespace ReceiverBackend
{
    public static class ReceiverBackEndDependencyInjection
    {
        public static void Register(ServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IMessageReceiverService, MessageReceiverService>();
            serviceCollection.AddScoped<IMessageProcessingService, MessageProcessingService>();
            CommonDependencyInjection.Register(serviceCollection);
        }
    }
}
