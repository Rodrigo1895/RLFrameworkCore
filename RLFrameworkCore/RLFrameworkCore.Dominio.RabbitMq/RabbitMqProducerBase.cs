using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RLFrameworkCore.Dominio.RabbitMq.Interfaces;
using System.Text;
using System.Text.Json;

namespace RLFrameworkCore.Dominio.RabbitMq
{
    public abstract class RabbitMqProducerBase<T> : IRabbitMqProducer<T>
    {
        private readonly RabbitMqConnection _rabbitMqConnection;
        protected abstract string ExchangeName { get; }
        protected abstract string RoutingKeyName { get; }
        private string AppId { get; }

        protected RabbitMqProducerBase(RabbitMqConnection rabbitMqConnection,
            IOptions<RabbitMqConfig> optionsRabbitMqConfig)
        {
            _rabbitMqConnection = rabbitMqConnection;
            AppId = optionsRabbitMqConfig.Value.AppId;
        }

        protected abstract void ConfigInit(IModel channel);

        public void Publish(T message)
        {
            if (_rabbitMqConnection.ConnectionExists())
            {
                using var channel = _rabbitMqConnection.CreateChanel();
                ConfigInit(channel);

                var body = GetMessageAsByteArray(message);
                var properties = channel.CreateBasicProperties();
                properties.AppId = AppId;
                properties.ContentType = "application/json";
                properties.Persistent = true;
                properties.Timestamp = new AmqpTimestamp(DateTimeOffset.UtcNow.ToUnixTimeSeconds());

                channel.BasicPublish(exchange: ExchangeName, routingKey: RoutingKeyName, body: body, basicProperties: properties);
            }
        }

        private byte[] GetMessageAsByteArray(T message)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
            };
            var json = JsonSerializer.Serialize<T>((T)message, options);
            var body = Encoding.UTF8.GetBytes(json);
            return body;
        }
    }
}
