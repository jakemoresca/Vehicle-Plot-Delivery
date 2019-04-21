using RabbitMQ.Client;

namespace Common.Factories
{
    public interface IPlotConnectionFactory
    {
        IConnection GetOrCreate(string uriString);
    }
}