using Exemplo.Web.Dto.Cliente.Request;
using Exemplo.Web.Dto.Cliente.Response;
using RLFrameworkCore.Dominio.Servico.Interfaces;
using RLFrameworkCore.Dto.DTOs.Interfaces;

namespace Exemplo.Web.Aplicacao.Servicos.Cliente
{
    public interface IClienteAppServico : IAppServicoBase
    {
        Task<ClienteDto> Adicionar(AdicionarClienteDto dto, CancellationToken cancellationToken);
        Task<ClienteDto> Atualizar(int idCliente, AtualizarClienteDto dto, CancellationToken cancellationToken);
        Task<bool> Deletar(int idCliente, CancellationToken cancellationToken);
        Task<ClienteDto> BuscarPorId(int idCliente);
        Task<ClienteDto> BuscarPorCpf(string cpf);
        Task<IListDto<ClienteDto>> Buscar(BuscarClientesDto dto);
    }
}
