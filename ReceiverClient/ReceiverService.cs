using Microsoft.Extensions.Logging;
using ReceiverBackend.Services;
using System.ServiceProcess;

namespace ReceiverClient
{
    internal class ReceiverService : ServiceBase
    {
        private readonly IMessageReceiverService _messageReceiverService;
        private readonly ILogger<ReceiverService> _logger;

        public ReceiverService(IMessageReceiverService messageReceiverService, ILogger<ReceiverService> logger)
        {
            ServiceName = "ReceiverService";
            _messageReceiverService = messageReceiverService;
            _logger = logger;
        }

        protected override void OnStart(string[] args)
        {
            _logger.LogInformation("Starting receiver service.");
            _messageReceiverService.StartReceivingMessage();
            base.OnStart(args);
        }

        protected override void OnStop()
        {
            _logger.LogInformation("Stopping receiver service.");
            _messageReceiverService.StopReceivingMessage();
            base.OnStop();  
        }
    }
}
