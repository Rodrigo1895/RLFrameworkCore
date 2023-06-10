using AutoMapper;
using Exemplo.Web.Dominio.Entidades.Pedido;
using Exemplo.Web.Dto.Pedido.Request;
using Exemplo.Web.Dto.Pedido.Response;

namespace Exemplo.Web.Aplicacao.AutoMapper.Profiles.Pedido
{
    public class PedidoItemProfile : Profile
    {
        public PedidoItemProfile()
        {
            CreateMap<PedidoItemEntidade, PedidoItemDto>();
            CreateMap<AdicionarPedidoItemDto, PedidoItemEntidade>();
        }
    }
}
