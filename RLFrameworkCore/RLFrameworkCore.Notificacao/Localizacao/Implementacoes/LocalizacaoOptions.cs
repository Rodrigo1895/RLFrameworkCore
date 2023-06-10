using System.Reflection;
using RLFrameworkCore.Notificacao.Localizacao.Interfaces;

namespace RLFrameworkCore.Notificacao.Localizacao.Implementacoes
{
    internal class LocalizacaoOptions : ILocalizacaoOptions
    {
        private ILocalizacaoConfig _config;

        public LocalizacaoOptions(ILocalizacaoConfig config)
        {
            _config = config;
        }


        public ILocalizacaoOptions AddJsonLocalizacaoArquivo(string nomeArquivo, Assembly assembly, string caminhoArquivo)
        {
            _config.AddJsonLocalizacaoArquivo(nomeArquivo, assembly, caminhoArquivo);

            return this;
        }

        public ILocalizacaoOptions AddIdioma(string nome, bool padrao = false)
        {
            _config.AddIdioma(nome, padrao);

            return this;
        }
    }
}
