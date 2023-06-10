using MediatR;

namespace Exemplo.Web.Aplicacao.Servicos.Cliente.Commands
{
    public class DeletarClienteCommand : IRequest<bool>
    {
        public int IdCliente { get; set; }
    }
}
