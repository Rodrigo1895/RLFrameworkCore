using Exemplo.Web.Aplicacao.AutoMapper.Profiles.Cliente;
using Exemplo.Web.Aplicacao.AutoMapper.Profiles.Pedido;
using Exemplo.Web.Aplicacao.AutoMapper.Profiles.Produto;
using Exemplo.Web.Aplicacao.AutoMapper.Profiles.RabbitMq;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class AutoMapperConfiguracao
    {
        public static void AddAutoMapperConfiguracao(this IServiceCollection services)
        {
            services.AddAutoMapper(
                
                typeof(ClienteProfile),
                typeof(ProdutoProfile),
                typeof(PedidoProfile),
                typeof(PedidoItemProfile),

                // RabbitMq Messages
                typeof(PedidoConcluidoMessageProfile)
            );
        }
    }
}