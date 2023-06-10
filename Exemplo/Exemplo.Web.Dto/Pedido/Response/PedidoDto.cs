using Exemplo.Web.Dto.Cliente.Response;
using RLFrameworkCore.Dto.DTOs;

namespace Exemplo.Web.Dto.Pedido.Response
{
    public class PedidoDto : DtoBase
    {
        public int IdPedido { get; set; }
        public string Status { get; set; }
        public DateTime DataHoraInclusao { get; set; }
        public DateTime? DataHoraConclusao { get; set; }
        public ClienteDto Cliente { get; set; }
        public IList<PedidoItemDto> PedidoItens { get; set; }
    }
}
