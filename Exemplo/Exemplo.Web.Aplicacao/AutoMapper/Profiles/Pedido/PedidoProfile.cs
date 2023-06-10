using AutoMapper;
using Exemplo.Web.Dominio.Entidades.Pedido;
using Exemplo.Web.Dominio.Entidades.Pedido.Enums;
using Exemplo.Web.Dto.Pedido.Request;
using Exemplo.Web.Dto.Pedido.Response;

namespace Exemplo.Web.Aplicacao.AutoMapper.Profiles.Pedido
{
    public class PedidoProfile : Profile
    {
        public PedidoProfile()
        {
            CreateMap<PedidoEntidade, PedidoDto>();

            CreateMap<AdicionarPedidoDto, PedidoEntidade>()
                .ForMember(dest => dest.DataHoraInclusao, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => PedidoStatusEnum.A.ToString()));
        }
    }
}
