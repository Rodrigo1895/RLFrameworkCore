using System.Reflection;

namespace RLFrameworkCore.Notificacao.Localizacao.Interfaces
{
    public interface ILocalizacaoOptions
    {
        ILocalizacaoOptions AddJsonLocalizacaoArquivo(string nomeArquivo, Assembly assembly, string caminhoArquivo);
        ILocalizacaoOptions AddIdioma(string nome, bool padrao = false);
    }
}
