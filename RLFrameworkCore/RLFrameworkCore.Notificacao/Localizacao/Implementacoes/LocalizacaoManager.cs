using System.Globalization;
using RLFrameworkCore.Notificacao.Localizacao.Interfaces;

namespace RLFrameworkCore.Notificacao.Localizacao.Implementacoes
{
    internal class LocalizacaoManager : ILocalizacaoManager
    {
        private ILocalizacaoConfig _config;
        public LocalizacaoManager(ILocalizacaoConfig config)
        {
            _config = config;
        }

        public ILocalizacaoArquivo GetSource(string nome)
        {
            var source = _config.Arquivos.FirstOrDefault(x => x.Nome == nome);
            if (source == null)
                throw new ArgumentException($"Source \" { nome } \" não definido na inicialização");            

            return source;
        }
        public CultureInfo GetCultureInfo()
        {
            CultureInfo cultureInfo = CultureInfo.CurrentCulture;
            var idioma = _config.Idiomas.FirstOrDefault(x => x.CultureInfo.Name == cultureInfo.Name);
            if (idioma == null)
            {
                idioma = _config.Idiomas.FirstOrDefault(x => x.Padrao);
            }

            cultureInfo = idioma?.CultureInfo;

            return cultureInfo;
        }
    }
}
