using Exemplo.Web.Dto.Cliente.Request;
using Exemplo.Web.Dto.Cliente.Response;
using MediatR;

namespace Exemplo.Web.Aplicacao.Servicos.Cliente.Commands
{
    public class AdicionarClienteCommand : IRequest<ClienteDto>
    {
        public AdicionarClienteDto Dto { get; set; }
    }
}
