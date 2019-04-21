namespace ReceiverBackend.Services
{
    public interface IMessageReceiverService
    {
        void StartReceivingMessage();
        void StopReceivingMessage();
    }
}