using Exemplo.Web.Dto.Cliente.Request;
using Exemplo.Web.Dto.Cliente.Response;
using RLFrameworkCore.Dominio.Servico.Interfaces;
using RLFrameworkCore.Dto.DTOs.Interfaces;

namespace Exemplo.Web.Aplicacao.Servicos.Cliente
{
    public interface IClienteAppServico : IAppServicoBase
    {
        Task<ClienteDto> Adicionar(AdicionarClienteDto dto);
        Task<ClienteDto> Atualizar(int idCliente, AtualizarClienteDto dto);
        Task<bool> Deletar(int idCliente);
        Task<ClienteDto> BuscarPorId(int idCliente);
        Task<ClienteDto> BuscarPorCpf(string cpf);
        Task<IListDto<ClienteDto>> Buscar(BuscarClientesDto dto);
    }
}
