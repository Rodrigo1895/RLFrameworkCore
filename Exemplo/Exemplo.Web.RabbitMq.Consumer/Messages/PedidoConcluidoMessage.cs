namespace Exemplo.Web.RabbitMq.Consumer.Messages
{
    public class PedidoConcluidoMessage
    {
        public int IdPedido { get; set; }
        public DateTime DataHoraConclusao { get; set; }
        public int IdCliente { get; set; }
        public IList<PedidoItemConcluidoMessage> PedidoItens { get; set; }
    }

    public class PedidoItemConcluidoMessage
    {
        public int IdItem { get; set; }
        public int IdProduto { get; set; }
        public decimal Quantidade { get; set; }
        public decimal ValorUnit { get; set; }
    }
}
