using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace RLFrameworkCore.Dominio.RabbitMq
{
    public abstract class RabbitMqConsumerBase<T> : BackgroundService
    {
        private RabbitMqConnection _rabbitMqConnection;
        private IModel _channel;
        protected abstract string QueueName { get; }

        protected RabbitMqConsumerBase(RabbitMqConnection rabbitMqConnection)
        {
            _rabbitMqConnection = rabbitMqConnection;
            if (_rabbitMqConnection.ConnectionExists())
            {
                _channel = _rabbitMqConnection.CreateChanel();
                ConfigInit(_channel);
            }
        }

        protected abstract void ConfigInit(IModel channel);
        protected abstract Task ProcessObject(T obj);

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (object sender, BasicDeliverEventArgs evt) =>
            {
                var content = Encoding.UTF8.GetString(evt.Body.ToArray());
                var obj = JsonSerializer.Deserialize<T>(content);
                ProcessObject(obj).GetAwaiter().GetResult();
                _channel.BasicAck(evt.DeliveryTag, false);
            };
            _channel.BasicConsume(QueueName, false, consumer);
            return Task.CompletedTask;
        }
    }
}
