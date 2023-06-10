using Exemplo.Web.Dominio.Entidades.Pedido;
using Exemplo.Web.Dto.Pedido.Request;
using Exemplo.Web.Dto.Pedido.Response;
using RLFrameworkCore.Dto.DTOs.Interfaces;
using RLFrameworkCore.Repositorio.Repositorio.Interfaces;

namespace Exemplo.Web.Dominio.Interfaces.Repositorios
{
    public interface IPedidoRepositorio : IRepositorio<PedidoEntidade>, IRepositorioLeitura<PedidoEntidade>
    {
        Task<PedidoDto> BuscarPedidoCompletoPorId(int idPedido, CancellationToken cancellationToken = default);
        Task<IListDto<PedidoDto>> BuscarPedidos(BuscarPedidosDto dto, CancellationToken cancellationToken = default);        
    }
}