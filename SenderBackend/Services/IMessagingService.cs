namespace SenderBackend.Services
{
    public interface IMessagingService
    {
        void SendMessage(byte[] messageBody);
    }
}