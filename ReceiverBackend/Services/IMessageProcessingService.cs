namespace ReceiverBackend.Services
{
    public interface IMessageProcessingService
    {
        void Process(byte[] messageBody);
    }
}