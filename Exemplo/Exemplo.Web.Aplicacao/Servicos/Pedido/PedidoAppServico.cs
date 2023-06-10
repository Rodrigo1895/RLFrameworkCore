using Exemplo.Web.Aplicacao.Commands.Pedido;
using Exemplo.Web.Aplicacao.Servicos.Pedido.Validacoes;
using Exemplo.Web.Dominio.Interfaces.Repositorios;
using Exemplo.Web.Dto.Pedido.Request;
using Exemplo.Web.Dto.Pedido.Response;
using MediatR;
using RLFrameworkCore.Dominio.Servico;
using RLFrameworkCore.Dto.DTOs.Interfaces;
using RLFrameworkCore.Notificacao.Notificacao.Interfaces;

namespace Exemplo.Web.Aplicacao.Servicos.Pedido
{
    public class PedidoAppServico : AppServicoBase, IPedidoAppServico
    {
        private readonly IPedidoRepositorio _pedidoRepositorio;
        private readonly IMediator _mediator;


        public PedidoAppServico(INotificacaoContexto notificacao,
            IPedidoRepositorio pedidoRepositorio,
            IMediator mediator) : base(notificacao)
        {
            _mediator = mediator;
            _pedidoRepositorio = pedidoRepositorio;
        }

        public async Task<PedidoDto> AdicionarPedido(AdicionarPedidoDto dto)
        {
            var command = new AdicionarPedidoCommand
            {
                Dto = dto
            };

            var pedido = await _mediator.Send(command);
            return pedido != null ? await BuscarPorId(pedido.IdPedido) : null;
        }

        public async Task<PedidoDto> ConcluirPedido(int idPedido)
        {
            var command = new ConcluirPedidoCommand
            {
                IdPedido = idPedido
            };

            var pedido = await _mediator.Send(command);
            return pedido != null ? await BuscarPorId(pedido.IdPedido) : null;
        }

        public async Task<PedidoDto> BuscarPorId(int idPedido)
        {
            return await _pedidoRepositorio.BuscarPedidoCompletoPorId(idPedido);
        }

        public async Task<IListDto<PedidoDto>> Buscar(BuscarPedidosDto dto)
        {
            if (!await ValidarDto(new ValidarBuscarPedidosDto(), dto))
                return null;

            return await _pedidoRepositorio.BuscarPedidos(dto);
        }
    }
}