using ReceiverBackend.Services;
using System.ServiceProcess;

namespace ReceiverClient
{
    internal class ReceiverService : ServiceBase
    {
        private readonly IMessageReceiverService _messageReceiverService;

        public ReceiverService(IMessageReceiverService messageReceiverService)
        {
            ServiceName = "ReceiverService";
            _messageReceiverService = messageReceiverService;
        }

        protected override void OnStart(string[] args)
        {
            _messageReceiverService.StartReceivingMessage();
            base.OnStart(args);
        }

        protected override void OnStop()
        {
            _messageReceiverService.StopReceivingMessage();
            base.OnStop();  
        }
    }
}
