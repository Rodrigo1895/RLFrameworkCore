using Microsoft.Extensions.Caching.Memory;
using System.Reflection;
using RLFrameworkCore.Notificacao.Localizacao.Interfaces;

namespace RLFrameworkCore.Notificacao.Localizacao.Implementacoes
{
    internal class LocalizacaoConfig : ILocalizacaoConfig
    {
        private readonly List<IIdiomaInfo> _idiomas;
        private readonly List<ILocalizacaoArquivo> _arquivos;

        public IReadOnlyCollection<IIdiomaInfo> Idiomas => _idiomas;
        public IReadOnlyCollection<ILocalizacaoArquivo> Arquivos => _arquivos;
        private IMemoryCache _memoryCache { get; }

        public LocalizacaoConfig(IMemoryCache memoryCache)
        {
            _idiomas = new List<IIdiomaInfo>();
            _arquivos = new List<ILocalizacaoArquivo>();
            _memoryCache = memoryCache;
        }

        public ILocalizacaoConfig AddJsonLocalizacaoArquivo(string nomeArquivo, Assembly assembly, string localArquivo)
        {
            _arquivos.Add(new LocalizacaoArquivo(nomeArquivo, assembly, localArquivo, _memoryCache));
            return this;
        }

        public ILocalizacaoConfig AddIdioma(string nome, bool padrao = false)
        {
            _idiomas.Add(new IdiomaInfo(nome, padrao));
            return this;
        }
    }
}
