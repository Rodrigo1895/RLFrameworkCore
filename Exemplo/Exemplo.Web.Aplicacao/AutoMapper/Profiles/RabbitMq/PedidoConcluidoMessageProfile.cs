using AutoMapper;
using Exemplo.Web.Aplicacao.RabbitMq.Messages;
using Exemplo.Web.Dto.Pedido.Response;

namespace Exemplo.Web.Aplicacao.AutoMapper.Profiles.RabbitMq
{
    public class PedidoConcluidoMessageProfile : Profile
    {
        public PedidoConcluidoMessageProfile()
        {
            CreateMap<PedidoDto, PedidoConcluidoMessage>();
            CreateMap<PedidoItemDto, PedidoItemConcluidoMessage>()
                .ForMember(dest => dest.IdProduto, opt => opt.MapFrom(src => src.Produto.IdProduto));
        }
    }
}
