using RabbitMQ.Client;
using System;

namespace Common.Factories
{
    public class PlotConnectionFactory : IPlotConnectionFactory
    {
        private IConnection _connection;

        public IConnection GetOrCreate(string uriString)
        {
            if (_connection != null)
                return _connection;

            Uri.TryCreate(uriString, UriKind.Absolute, out var uri);

            var connectionFactory = new ConnectionFactory
            {
                Uri = uri
            };

            _connection = connectionFactory.CreateConnection();

            return _connection;
        }
    }
}
