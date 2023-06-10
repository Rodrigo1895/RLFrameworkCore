using Microsoft.Extensions.Caching.Memory;
using RLFrameworkCore.Notificacao.Localizacao.Implementacoes;
using RLFrameworkCore.Notificacao.Localizacao.Interfaces;

namespace Microsoft.Extensions.DependencyInjection
{
    internal static class LocalizacaoExtensions
    {
        internal static IServiceCollection AddLocalizacao(this IServiceCollection services, Action<ILocalizacaoOptions> configure)
        {
            services.AddMemoryCache();

            IMemoryCache memorycache = services.BuildServiceProvider().GetRequiredService<IMemoryCache>();

            ILocalizacaoConfig config = new LocalizacaoConfig(memorycache);
            ILocalizacaoOptions options = new LocalizacaoOptions(config);
            configure(options);

            if (config.Idiomas?.Where(x => x.Padrao).ToList().Count == 0)
                throw new ArgumentException("Não foi definido um idioma padrão na inicialização.");

            if (config.Idiomas?.Where(x => x.Padrao).ToList().Count > 1)
                throw new ArgumentException("Foi definido mais de um idioma padrão na inicialização.");

            services.AddSingleton<ILocalizacaoConfig>(config);
            services.AddScoped<ILocalizacaoManager, LocalizacaoManager>();

            return services;
        }
    }
}
