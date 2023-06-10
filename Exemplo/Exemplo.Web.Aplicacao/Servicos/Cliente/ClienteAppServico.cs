using Exemplo.Web.Aplicacao.Servicos.Cliente.Commands;
using Exemplo.Web.Aplicacao.Servicos.Cliente.Validacoes;
using Exemplo.Web.Dominio.Interfaces.Repositorios;
using Exemplo.Web.Dto.Cliente.Request;
using Exemplo.Web.Dto.Cliente.Response;
using MediatR;
using RLFrameworkCore.Dominio.Servico;
using RLFrameworkCore.Dto.DTOs.Interfaces;
using RLFrameworkCore.Notificacao.Notificacao.Interfaces;

namespace Exemplo.Web.Aplicacao.Servicos.Cliente
{
    public class ClienteAppServico : AppServicoBase, IClienteAppServico
    {
        private readonly IClienteRepositorio _clienteRepositorio;
        private readonly IMediator _mediator;
        public ClienteAppServico(INotificacaoContexto notificacao,
            IClienteRepositorio clienteRepositorio,
            IMediator mediator) : base(notificacao)
        {
            _clienteRepositorio = clienteRepositorio;
            _mediator = mediator;
        }

        public async Task<ClienteDto> Adicionar(AdicionarClienteDto dto, CancellationToken cancellationToken)
        {
            var command = new AdicionarClienteCommand
            {
                Dto = dto
            };

            return await _mediator.Send(command, cancellationToken);
        }

        public async Task<ClienteDto> Atualizar(int idCliente, AtualizarClienteDto dto, CancellationToken cancellationToken)
        {
            var command = new AtualizarClienteCommand
            {
                IdCliente = idCliente,
                Dto = dto
            };

            return await _mediator.Send(command, cancellationToken);
        }

        public async Task<bool> Deletar(int idCliente, CancellationToken cancellationToken)
        {
            var command = new DeletarClienteCommand
            {
                IdCliente = idCliente
            };

            return await _mediator.Send(command, cancellationToken);
        }

        public async Task<ClienteDto> BuscarPorId(int idCliente)
        {
            var cliente = await _clienteRepositorio.BuscarAsync(x => x.IdCliente == idCliente);
            return cliente.MapTo<ClienteDto>();
        }

        public async Task<ClienteDto> BuscarPorCpf(string cpf)
        {
            var cliente = await _clienteRepositorio.BuscarAsync(x => x.CPF == cpf);
            return cliente.MapTo<ClienteDto>();
        }

        public async Task<IListDto<ClienteDto>> Buscar(BuscarClientesDto dto)
        {
            if (!await ValidarDto(new ValidarBuscarClientesDto(), dto))
                return null;

            return await _clienteRepositorio.BuscarClientes(dto);
        }        
    }
}