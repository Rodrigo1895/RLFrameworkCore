using Exemplo.Web.Aplicacao.RabbitMq.Messages;
using Exemplo.Web.Aplicacao.RabbitMq.Producers;
using Exemplo.Web.Aplicacao.Servicos.Cliente;
using Exemplo.Web.Aplicacao.Servicos.Pedido;
using RLFrameworkCore.Dominio.RabbitMq.Interfaces;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class AppServicosExtensions
    {
        public static IServiceCollection AddAppServicosDependencia(this IServiceCollection services)
        {
            services.AddAutoMapperConfiguracao();
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

            #region AppServicos
            services.AddScoped<IPedidoAppServico, PedidoAppServico>();
            services.AddScoped<IClienteAppServico, ClienteAppServico>();
            #endregion

            #region RabbitMq
            services.AddSingleton<IRabbitMqProducer<PedidoConcluidoMessage>, RabbitMqProducerPedidoConcluido>();
            #endregion

            return services;
        }        
    }
}
