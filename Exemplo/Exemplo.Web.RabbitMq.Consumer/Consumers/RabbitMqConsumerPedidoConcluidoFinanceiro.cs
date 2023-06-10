using Exemplo.Web.RabbitMq.Consumer.Messages;
using RabbitMQ.Client;
using RLFrameworkCore.Dominio.RabbitMq;
using System.Text.Json;

namespace Exemplo.Web.RabbitMq.Consumer.Consumers
{
    public class RabbitMqConsumerPedidoConcluidoFinanceiro : RabbitMqConsumerBase<PedidoConcluidoMessage>
    {

        public RabbitMqConsumerPedidoConcluidoFinanceiro(RabbitMqConnection rabbitMqConnection) : base(rabbitMqConnection)
        {
        }

        protected override string QueueName => "notificarFinanceiro";

        protected override void ConfigInit(IModel channel)
        {
            var exchange = "pedidos";
            var routingKey = "pedido.concluido";

            channel.ExchangeDeclare(exchange: exchange, type: ExchangeType.Direct, durable: true);

            channel.QueueDeclare(queue: QueueName, durable: true, exclusive: false, autoDelete: false, arguments: null);
            channel.QueueBind(queue: QueueName, exchange: exchange, routingKey: routingKey);
        }

        protected override Task ProcessObject(PedidoConcluidoMessage obj)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
            };
            var json = JsonSerializer.Serialize(obj, options);

            Console.WriteLine($"##### Queue {QueueName} #####\n" +
                              $"{json}\n" +
                              $"##### Fim Queue {QueueName} #####\n");

            return Task.CompletedTask;
        }
    }
}
