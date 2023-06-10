using ExemploAuth.Web.Aplicacao.Servicos.Autenticacao;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class AppServicosExtensions
    {
        public static IServiceCollection AddAppServicosDependencia(this IServiceCollection services)
        {
            services.AddAutoMapperConfiguracao();

            services.AddScoped<IGerarToken, GerarTokenAsymmetric>();
            services.AddScoped<IAutenticacaoAppServico, AutenticacaoAppServico>();
            return services;
        }
    }
}
