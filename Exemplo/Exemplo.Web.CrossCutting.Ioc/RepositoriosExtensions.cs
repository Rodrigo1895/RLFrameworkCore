using Exemplo.Web.Data.Repositorios.Cliente;
using Exemplo.Web.Data.Repositorios.Pedido;
using Exemplo.Web.Dominio.Interfaces.Repositorios;

namespace Microsoft.Extensions.DependencyInjection
{
    internal static class RepositoriosExtensions
    {
        internal static IServiceCollection RegistraRepositorios(this IServiceCollection services)
        {
            services.AddScoped<IPedidoRepositorio, PedidoRepositorio>();
            services.AddScoped<IClienteRepositorio, ClienteRepositorio>();

            return services;
        }
    }
}
