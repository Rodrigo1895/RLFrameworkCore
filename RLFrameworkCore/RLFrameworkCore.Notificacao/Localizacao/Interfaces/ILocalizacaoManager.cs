using System.Globalization;

namespace RLFrameworkCore.Notificacao.Localizacao.Interfaces
{
    internal interface ILocalizacaoManager
    {
        ILocalizacaoArquivo GetSource(string nome);
        CultureInfo GetCultureInfo();
    }
}
