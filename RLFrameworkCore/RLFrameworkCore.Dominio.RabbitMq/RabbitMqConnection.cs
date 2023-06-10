using RabbitMQ.Client;

namespace RLFrameworkCore.Dominio.RabbitMq
{
    public sealed class RabbitMqConnection
    {
        private readonly ConnectionFactory _connectionFactory;
        private static IConnection _connection;

        public RabbitMqConnection(ConnectionFactory connectionFactory,
            IConnection connection)
        {
            _connectionFactory = connectionFactory;
            _connection = connection;
        }

        public bool ConnectionExists()
        {
            if (_connection != null) return true;
            CreateConnection();
            return _connection != null;
        }

        private void CreateConnection()
        {
            _connection = _connectionFactory.CreateConnection();
        }

        public IModel CreateChanel()
        {
            return _connection.CreateModel();
        }
    }
}
