using Exemplo.Web.Dto.Produto.Response;
using RLFrameworkCore.Dto.DTOs;

namespace Exemplo.Web.Dto.Pedido.Response
{
    public class PedidoItemDto : DtoBase
    {
        public int IdItem { get; set; }
        public decimal Quantidade { get; set; }
        public decimal ValorUnit { get; set; }
        public ProdutoDto Produto { get; set; }
    }
}
