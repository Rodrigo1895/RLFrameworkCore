using Exemplo.Web.Aplicacao.RabbitMq.Messages;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RLFrameworkCore.Dominio.RabbitMq;

namespace Exemplo.Web.Aplicacao.RabbitMq.Producers
{
    public class RabbitMqProducerPedidoConcluido : RabbitMqProducerBase<PedidoConcluidoMessage>
    {
        public RabbitMqProducerPedidoConcluido(RabbitMqConnection rabbitMqConnection, 
            IOptions<RabbitMqConfig> optionsRabbitMqConfig) : base(rabbitMqConnection, optionsRabbitMqConfig)
        {
        }

        protected override string ExchangeName => "pedidos";
        protected override string RoutingKeyName => "pedido.concluido";

        protected override void ConfigInit(IModel channel)
        {
            var queueNotificarFinanceiro = "notificarFinanceiro";
            var queueEnviarEmailCliente = "enviarEmailCliente";

            channel.ExchangeDeclare(exchange: ExchangeName, type: ExchangeType.Direct, durable: true);

            channel.QueueDeclare(queue: queueNotificarFinanceiro, durable: true, exclusive: false, autoDelete: false, arguments: null);
            channel.QueueBind(queue: queueNotificarFinanceiro, exchange: ExchangeName, routingKey: RoutingKeyName);

            channel.QueueDeclare(queue: queueEnviarEmailCliente, durable: true, exclusive: false, autoDelete: false, arguments: null);
            channel.QueueBind(queue: queueEnviarEmailCliente, exchange: ExchangeName, routingKey: RoutingKeyName);
        }
    }
}