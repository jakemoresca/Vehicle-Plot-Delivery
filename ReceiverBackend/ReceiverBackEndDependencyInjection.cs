using Common.Storage;
using Microsoft.Extensions.DependencyInjection;
using ReceiverBackend.Services;

namespace ReceiverBackend
{
    public static class ReceiverBackEndDependencyInjection
    {
        public static void Register(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IMessageReceiverService, MessageReceiverService>();
            serviceCollection.AddScoped<IMessageProcessingService, MessageProcessingService>();
            CommonStorageDepedencyInjection.Register(serviceCollection);
        }
    }
}
