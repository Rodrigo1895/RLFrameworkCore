using RLFrameworkCore.Notificacao.Localizacao.Interfaces;
using RLFrameworkCore.Notificacao.Notificacao.Interfaces;
using System.Globalization;

namespace RLFrameworkCore.Notificacao.Notificacao.Implementacoes
{
    internal class NotificacaoContexto : INotificacaoContexto
    {

        private readonly List<NotificacaoModel> _notificacoes;
        public IReadOnlyCollection<INotificacaoModel> Notificacoes => _notificacoes;
        public bool HasNotificacoes => _notificacoes.Count > 0;

        private readonly ILocalizacaoManager _localizacaoManager;

        private CultureInfo idioma;

        public NotificacaoContexto(ILocalizacaoManager localizacaoManager)
        {
            _notificacoes = new List<NotificacaoModel>();
            _localizacaoManager = localizacaoManager;
        }

        public void AddNotificacao(string chave, string enumString, string nomeArquivo)
        {
            var source = _localizacaoManager.GetSource(nomeArquivo);
            if (idioma == null)
                idioma = _localizacaoManager.GetCultureInfo();

            var descMensagem = source.GetString(enumString, idioma);            
            
            _notificacoes.Add(new NotificacaoModel(ToCamelCase(chave), $"{descMensagem} - [{enumString}]"));
        }

        public void AddNotificacao(string chave, Enum @enum, string nomeArquivo)
        {
            AddNotificacao(chave, @enum.ToString(), nomeArquivo);
        }

        public void AddNotificacao(string chave, string mensagem)
        {            
            _notificacoes.Add(new NotificacaoModel(ToCamelCase(chave), mensagem));
        }

        private string ToCamelCase(string str) =>
         string.IsNullOrEmpty(str) || str.Length < 2
         ? str
         : char.ToLowerInvariant(str[0]) + str.Substring(1);
    }
}