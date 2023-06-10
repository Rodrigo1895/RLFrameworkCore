using Exemplo.Web.Dominio.Entidades.Cliente;
using Exemplo.Web.Dto.Cliente.Request;
using Exemplo.Web.Dto.Cliente.Response;
using RLFrameworkCore.Dto.DTOs.Interfaces;
using RLFrameworkCore.Repositorio.Repositorio.Interfaces;

namespace Exemplo.Web.Dominio.Interfaces.Repositorios
{
    public interface IClienteRepositorio : IRepositorio<ClienteEntidade>, IRepositorioLeitura<ClienteEntidade>
    {
        Task<IListDto<ClienteDto>> BuscarClientes(BuscarClientesDto dto, CancellationToken cancellationToken = default);
    }
}