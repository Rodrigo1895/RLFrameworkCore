using RLFrameworkCore.Dto.DTOs;
using System.Text.Json.Serialization;

namespace Exemplo.Web.Dto.Pedido.Request
{
    public class AdicionarPedidoDto : DtoBase
    {
        public int IdCliente { get; set; }
        public IList<AdicionarPedidoItemDto> PedidoItens { get; set; }
    }

    public class AdicionarPedidoItemDto : DtoBase
    {
        public int IdProduto { get; set; }
        [JsonIgnore]
        public int IdItem { get; set; }
        public decimal Quantidade { get; set; }
        public decimal ValorUnit { get; set; }
    }
}
