using AutoMapper;
using Exemplo.Web.Aplicacao.RabbitMq.Messages;
using Exemplo.Web.Dominio.Entidades.Pedido;

namespace Exemplo.Web.Aplicacao.AutoMapper.Profiles.RabbitMq
{
    public class PedidoConcluidoMessageProfile : Profile
    {
        public PedidoConcluidoMessageProfile()
        {
            CreateMap<PedidoEntidade, PedidoConcluidoMessage>();
            CreateMap<PedidoItemEntidade, PedidoItemConcluidoMessage>();
        }
    }
}
