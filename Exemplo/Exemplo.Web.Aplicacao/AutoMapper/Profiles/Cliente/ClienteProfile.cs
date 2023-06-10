using AutoMapper;
using Exemplo.Web.Dominio.Entidades.Cliente;
using Exemplo.Web.Dto.Cliente.Request;
using Exemplo.Web.Dto.Cliente.Response;

namespace Exemplo.Web.Aplicacao.AutoMapper.Profiles.Cliente
{
    public class ClienteProfile : Profile
    {
        public ClienteProfile()
        {
            CreateMap<ClienteEntidade, ClienteDto>();

            CreateMap<AdicionarClienteDto, ClienteEntidade>();
            CreateMap<AtualizarClienteDto, ClienteEntidade>();
        }
    }
}
