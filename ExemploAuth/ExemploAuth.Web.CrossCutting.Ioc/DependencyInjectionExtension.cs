using ExemploAuth.Web.Data.Contextos;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DependencyInjectionExtension
    {
        public static IServiceCollection AddDependencyInjectionExtension(this IServiceCollection services, IConfiguration configuration)
        {
            services.RegistraConexaoBancoDados(configuration)
                .AddUnitOfWorkRepositorio<AuthContexto>();
            services.RegistraRepositorios();

            return services;
        }
    }
}