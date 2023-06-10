using MediatR;

namespace Exemplo.Web.Aplicacao.Commands.Cliente
{
    public class DeletarClienteCommand : IRequest<bool>
    {
        public int IdCliente { get; set; }
    }
}
