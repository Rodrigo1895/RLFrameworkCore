using System.Globalization;
using System.Reflection;

namespace RLFrameworkCore.Notificacao.Localizacao.Interfaces
{
    internal interface ILocalizacaoArquivo
    {
        string Nome { get; }

        Assembly AssemblyArquivo { get; }

        string Local { get; }

        string GetString(string nome, CultureInfo idioma);
    }
}
