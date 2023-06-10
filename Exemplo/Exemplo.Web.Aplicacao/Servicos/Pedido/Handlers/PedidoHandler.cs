using Exemplo.Web.Aplicacao.RabbitMq.Messages;
using Exemplo.Web.Aplicacao.Servicos.Pedido.Commands;
using Exemplo.Web.Aplicacao.Servicos.Pedido.Validacoes;
using Exemplo.Web.Data.Contextos;
using Exemplo.Web.Dominio;
using Exemplo.Web.Dominio.Entidades.Pedido;
using Exemplo.Web.Dominio.Entidades.Pedido.Enums;
using Exemplo.Web.Dominio.Interfaces.Repositorios;
using Exemplo.Web.Dominio.Localizacao;
using Exemplo.Web.Dto.Pedido.Response;
using MediatR;
using RLFrameworkCore.Dominio.RabbitMq.Interfaces;
using RLFrameworkCore.Dominio.Servico;
using RLFrameworkCore.Notificacao.Notificacao.Interfaces;
using RLFrameworkCore.Repositorio.UnitOfWork.Interfaces;

namespace Exemplo.Web.Aplicacao.Servicos.Pedido.Handlers
{
    public class PedidoHandler : HandlerBase,
        IRequestHandler<AdicionarPedidoCommand, PedidoDto>,
        IRequestHandler<ConcluirPedidoCommand, PedidoDto>
    {
        private readonly IPedidoRepositorio _pedidoRepositorio;
        private readonly IUnitOfWorkManager<AppContexto> _unitOfWork;
        private readonly IRabbitMqProducer<PedidoConcluidoMessage> _rabbitMqPedidoConcluido;

        public PedidoHandler(INotificacaoContexto notificacao,
            IUnitOfWorkManager<AppContexto> unitOfWork,
            IPedidoRepositorio pedidoRepositorio,
            IRabbitMqProducer<PedidoConcluidoMessage> rabbitMqPedidoConcluido) : base(notificacao)
        {
            _unitOfWork = unitOfWork;
            _pedidoRepositorio = pedidoRepositorio;
            _rabbitMqPedidoConcluido = rabbitMqPedidoConcluido;
        }

        public async Task<PedidoDto> Handle(AdicionarPedidoCommand request, CancellationToken cancellationToken)
        {
            if (!await ValidarDto(new ValidarAdicionarPedidoDto(), request.Dto))
                return null;

            int idItem = 0;
            foreach (var i in request.Dto.PedidoItens)
            {
                i.IdItem = ++idItem;
            }

            var pedidoEntidade = request.Dto.MapTo<PedidoEntidade>();

            if (!ValidarEntidade(pedidoEntidade))
                return null;

            await using (var controleTransacao = _unitOfWork.Begin())
            {
                var pedidoInserido = await _pedidoRepositorio.AdicionarAsync(pedidoEntidade, cancellationToken);

                if (await Commit(controleTransacao, cancellationToken))
                {
                    return await BuscarPorId(pedidoInserido.IdPedido, cancellationToken);
                }

                return null;
            }
        }

        public async Task<PedidoDto> Handle(ConcluirPedidoCommand request, CancellationToken cancellationToken)
        {
            var pedido = await _pedidoRepositorio.BuscarAsync(x => x.IdPedido == request.IdPedido, cancellationToken);
            if (pedido == null)
            {
                Notificacao.AddNotificacao("", EnumMensagensErro.PedidoNaoExiste, Constants.NomeErroSource);
                return null;
            }

            if (pedido.Status == PedidoStatusEnum.C.ToString())
            {
                Notificacao.AddNotificacao("", EnumMensagensErro.PedidoJaConcluido, Constants.NomeErroSource);
                return null;
            }
            pedido.ConcluirPedido();
            await _pedidoRepositorio.AtualizarAsync(pedido, cancellationToken);

            var pedidoDto = await BuscarPorId(pedido.IdPedido, cancellationToken);
            _rabbitMqPedidoConcluido.Publish(pedidoDto.MapTo<PedidoConcluidoMessage>());

            return pedidoDto;
        }

        private async Task<PedidoDto> BuscarPorId(int idPedido, CancellationToken cancellationToken)
        {
            return await _pedidoRepositorio.BuscarPedidoCompletoPorId(idPedido, cancellationToken);
        }
    }
}