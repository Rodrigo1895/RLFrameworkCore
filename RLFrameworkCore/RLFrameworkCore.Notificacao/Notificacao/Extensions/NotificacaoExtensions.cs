using RLFrameworkCore.Notificacao.Localizacao.Interfaces;
using RLFrameworkCore.Notificacao.Notificacao.Implementacoes;
using RLFrameworkCore.Notificacao.Notificacao.Interfaces;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class NotificacaoExtensions
    {
        public static IServiceCollection AddNotificacaoConfig(this IServiceCollection services, Action<ILocalizacaoOptions> configure)
        {
            services.AddLocalizacao(configure);

            services.AddScoped<INotificacaoContexto, NotificacaoContexto>();

            return services;
        }
    }
}