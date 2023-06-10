using Exemplo.Web.Dto.Pedido.Request;
using Exemplo.Web.Dto.Pedido.Response;
using MediatR;

namespace Exemplo.Web.Aplicacao.Commands.Pedido
{
    public class AdicionarPedidoCommand : IRequest<PedidoDto>
    {
        public AdicionarPedidoDto Dto{ get; set; }
    }
}
