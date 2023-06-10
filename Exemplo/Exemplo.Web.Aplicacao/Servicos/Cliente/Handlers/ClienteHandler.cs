using Exemplo.Web.Aplicacao.Servicos.Cliente.Commands;
using Exemplo.Web.Aplicacao.Servicos.Cliente.Validacoes;
using Exemplo.Web.Dominio;
using Exemplo.Web.Dominio.Entidades.Cliente;
using Exemplo.Web.Dominio.Interfaces.Repositorios;
using Exemplo.Web.Dominio.Localizacao;
using Exemplo.Web.Dto.Cliente.Response;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RLFrameworkCore.Dominio.Servico;
using RLFrameworkCore.Notificacao.Notificacao.Interfaces;

namespace Exemplo.Web.Aplicacao.Servicos.Cliente.Handlers
{
    public class ClienteHandler : HandlerBase,
        IRequestHandler<AdicionarClienteCommand, ClienteDto>,
        IRequestHandler<AtualizarClienteCommand, ClienteDto>,
        IRequestHandler<DeletarClienteCommand, bool>
    {

        private readonly IClienteRepositorio _clienteRepositorio;

        public ClienteHandler(INotificacaoContexto notificacao,
            IClienteRepositorio clienteRepositorio) : base(notificacao)
        {
            _clienteRepositorio = clienteRepositorio;
        }

        public async Task<ClienteDto> Handle(AdicionarClienteCommand request, CancellationToken cancellationToken)
        {
            if (!await ValidarDto(new ValidarAdicionarClienteDto(_clienteRepositorio), request.Dto))
                return null;

            var clienteEntidade = request.Dto.MapTo<ClienteEntidade>();
            if (!ValidarEntidade(clienteEntidade))
                return null;

            var clienteInserido = await _clienteRepositorio.AdicionarAsync(clienteEntidade, cancellationToken);
            return clienteInserido.MapTo<ClienteDto>();
        }

        public async Task<ClienteDto> Handle(AtualizarClienteCommand request, CancellationToken cancellationToken)
        {
            var cliente = await BuscarClienteValidar(request.IdCliente, cancellationToken);
            if (cliente == null)
                return null;

            cliente = request.Dto.MapTo(cliente);
            if (!ValidarEntidade(cliente))
                return null;
            cliente = await _clienteRepositorio.AtualizarAsync(cliente, cancellationToken);

            return cliente.MapTo<ClienteDto>();
        }

        public async Task<bool> Handle(DeletarClienteCommand request, CancellationToken cancellationToken)
        {
            var cliente = await BuscarClienteValidar(request.IdCliente, cancellationToken);
            if (cliente == null)
                return false;

            var retorno = await _clienteRepositorio.DeletarAsync(x => x.IdCliente == request.IdCliente, cancellationToken);

            return retorno;
        }

        private async Task<ClienteEntidade> BuscarClienteValidar(int idCliente, CancellationToken cancellationToken)
        {
            var cliente = await _clienteRepositorio.BuscarTodos(x => x.IdCliente == idCliente).FirstOrDefaultAsync(cancellationToken);

            if (cliente == null)
            {
                Notificacao.AddNotificacao("", EnumMensagensErro.ClienteNaoExiste, Constants.NomeErroSource);
                return null;
            }

            return cliente;
        }
    }
}
