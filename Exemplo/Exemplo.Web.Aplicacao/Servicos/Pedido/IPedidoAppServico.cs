using Exemplo.Web.Dto.Pedido.Request;
using Exemplo.Web.Dto.Pedido.Response;
using RLFrameworkCore.Dominio.Servico.Interfaces;
using RLFrameworkCore.Dto.DTOs.Interfaces;

namespace Exemplo.Web.Aplicacao.Servicos.Pedido
{
    public interface IPedidoAppServico : IAppServicoBase
    {
        Task<PedidoDto> AdicionarPedido(AdicionarPedidoDto dto);
        Task<PedidoDto> BuscarPorId(int idPedido);
        Task<IListDto<PedidoDto>> Buscar(BuscarPedidosDto dto);        
        Task<PedidoDto> ConcluirPedido(int idPedido);
    }
}