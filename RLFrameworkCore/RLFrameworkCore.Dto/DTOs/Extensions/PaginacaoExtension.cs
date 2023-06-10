using Microsoft.Extensions.Configuration;
using RLFrameworkCore.Dto.DTOs.Configuracoes;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class PaginacaoExtension
    {
        public static IServiceCollection AddPaginacaoConfig(this IServiceCollection services, IConfiguration configuration)
        {
            PaginacaoConfig.Configurar(int.Parse(configuration["PaginacaoPadrao:PaginaTamanhoPadrao"]),
                int.Parse(configuration["PaginacaoPadrao:PaginaTamanhoMaximo"]));

            return services;
        }
    }
}
