using Exemplo.Web.Dto.Cliente.Request;
using Exemplo.Web.Dto.Cliente.Response;
using MediatR;

namespace Exemplo.Web.Aplicacao.Commands.Cliente
{
    public class AtualizarClienteCommand : IRequest<ClienteDto>
    {
        public int IdCliente { get; set; }
        public AtualizarClienteDto Dto { get; set; }
    }
}
