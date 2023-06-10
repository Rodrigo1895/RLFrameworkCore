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
            channel.ExchangeDeclare(ExchangeName, ExchangeType.Direct, durable: true);

            channel.QueueDeclare(queue: "notificarFinanceiro", durable: true, exclusive: false, autoDelete: false, arguments: null);
            channel.QueueBind("notificarFinanceiro", ExchangeName, RoutingKeyName);

            channel.QueueDeclare(queue: "enviarEmailCliente", durable: true, exclusive: false, autoDelete: false, arguments: null);
            channel.QueueBind("enviarEmailCliente", ExchangeName, RoutingKeyName);
        }
    }
}