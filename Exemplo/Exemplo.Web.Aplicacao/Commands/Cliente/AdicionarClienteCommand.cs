using Exemplo.Web.Dto.Cliente.Request;
using Exemplo.Web.Dto.Cliente.Response;
using MediatR;

namespace Exemplo.Web.Aplicacao.Commands.Cliente
{
    public class AdicionarClienteCommand  : IRequest<ClienteDto>
    {
        public AdicionarClienteDto Dto { get; set; }
    }
}
