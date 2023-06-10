using System.Reflection;

namespace RLFrameworkCore.Notificacao.Localizacao.Interfaces
{
    internal interface ILocalizacaoConfig
    {
        IReadOnlyCollection<IIdiomaInfo> Idiomas { get; }
        IReadOnlyCollection<ILocalizacaoArquivo> Arquivos { get; }

        ILocalizacaoConfig AddJsonLocalizacaoArquivo(string nomeArquivo, Assembly assembly, string caminhoArquivo);
        ILocalizacaoConfig AddIdioma(string nome, bool padrao = false);
    }
}
