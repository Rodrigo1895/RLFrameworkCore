using Exemplo.Web.Data.Contextos;
using Exemplo.Web.Dominio.Entidades.Cliente;
using Exemplo.Web.Dominio.Interfaces.Repositorios;
using Exemplo.Web.Dto.Cliente.Request;
using Exemplo.Web.Dto.Cliente.Response;
using Microsoft.EntityFrameworkCore;
using RLFrameworkCore.Dto.DTOs.Interfaces;
using RLFrameworkCore.Repositorio.Repositorio;

namespace Exemplo.Web.Data.Repositorios.Cliente
{
    public class ClienteRepositorio : RepositorioBase<ClienteEntidade>, IClienteRepositorio
    {
        public ClienteRepositorio(AppContexto context) : base(context)
        {
        }

        public async Task<IListDto<ClienteDto>> BuscarClientes(BuscarClientesDto dto)
        {
            var query = BuscarTodos();

            if (!string.IsNullOrWhiteSpace(dto.Nome))
                query = query.Where(x => EF.Functions.Like(x.Nome.ToUpper(), $"%{dto.Nome.ToUpper()}%"));

            if(!string.IsNullOrWhiteSpace(dto.Genero))
                query = query.Where(x => x.Genero == dto.Genero);

            return await query.ToListDtoAsync<ClienteEntidade, ClienteDto>(dto);
        }
    }
}
