using Exemplo.Web.Dto.Pedido.Response;
using MediatR;

namespace Exemplo.Web.Aplicacao.Commands.Pedido
{
    public class ConcluirPedidoCommand : IRequest<PedidoDto>
    {
        public int IdPedido { get; set; }
    }
}