using Exemplo.Web.Data.Contextos;
using Exemplo.Web.Dominio.Entidades.Pedido;
using Exemplo.Web.Dominio.Interfaces.Repositorios;
using Exemplo.Web.Dto.Pedido.Request;
using Exemplo.Web.Dto.Pedido.Response;
using Microsoft.EntityFrameworkCore;
using RLFrameworkCore.Dto.DTOs.Interfaces;
using RLFrameworkCore.Repositorio.Repositorio;

namespace Exemplo.Web.Data.Repositorios.Pedido
{
    public class PedidoRepositorio : RepositorioBase<PedidoEntidade>, IPedidoRepositorio
    {
        public PedidoRepositorio(AppContexto context) : base(context)
        {
        }

        public async Task<PedidoDto> BuscarPedidoCompletoPorId(int idPedido, CancellationToken cancellationToken = default)
        {
            var pedido = await BuscarTodos(x => x.IdPedido == idPedido).AsNoTracking()
                .Include(x => x.Cliente)
                .Include(x => x.PedidoItens.OrderBy(y => y.IdItem)).ThenInclude(x => x.Produto)
                .FirstOrDefaultAsync(cancellationToken);

            return pedido.MapTo<PedidoDto>();
        }

        public async Task<IListDto<PedidoDto>> BuscarPedidos(BuscarPedidosDto dto, CancellationToken cancellationToken = default)
        {
            IQueryable<PedidoEntidade> query = BuscarTodos().OrderBy(x => x.IdPedido);

            #region Includes

            if (dto.RetornaCliente)
                query = query.Include(x => x.Cliente);

            if (dto.RetornaItens)
                query = query.Include(x => x.PedidoItens.OrderBy(y => y.IdItem)).ThenInclude(x => x.Produto);

            #endregion

            #region Filtros

            if (dto.Id != null)
                query = query.Where(x => x.IdPedido == dto.Id);

            if (dto.DataInicial != null)
                query = query.Where(x => x.DataHoraInclusao >= dto.DataInicial);

            if (dto.DataFinal != null)
                query = query.Where(x => x.DataHoraInclusao <= dto.DataFinal);

            if (!string.IsNullOrWhiteSpace(dto.Status))
                query = query.Where(x => x.Status.ToUpper().Contains(dto.Status.ToUpper()));

            #endregion

            return await query.ToListDtoAsync<PedidoEntidade, PedidoDto>(dto, cancellationToken);
        }       
    }
}