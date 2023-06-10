using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
    internal static class ConexaoBDExtensions
    {
        internal static IServiceCollection RegistraConexaoBancoDados(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddEfCoreSqlServer(configuration);

            return services;
        }
    }
}