using Exemplo.Web.Aplicacao.Commands.Pedido;
using Exemplo.Web.Aplicacao.RabbitMq.Messages;
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

namespace Exemplo.Web.Aplicacao.Handlers
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
                var pedidoInserido = await _pedidoRepositorio.AdicionarAsync(pedidoEntidade);

                if (await Commit(controleTransacao))
                {
                    return pedidoInserido.MapTo<PedidoDto>();
                }

                return null;
            }
        }

        public async Task<PedidoDto> Handle(ConcluirPedidoCommand request, CancellationToken cancellationToken)
        {
            var pedido = await _pedidoRepositorio.BuscarAsync(x => x.IdPedido == request.IdPedido);
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
            await _pedidoRepositorio.AtualizarAsync(pedido);

            _rabbitMqPedidoConcluido.Publish(pedido.MapTo<PedidoConcluidoMessage>());

            return pedido.MapTo<PedidoDto>();
        }
    }
}