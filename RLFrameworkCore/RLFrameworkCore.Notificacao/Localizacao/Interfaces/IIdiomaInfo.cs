using System.Globalization;

namespace RLFrameworkCore.Notificacao.Localizacao.Interfaces
{
    internal interface IIdiomaInfo
    {
        string Nome { get; }
        public bool Padrao { get; }
        public CultureInfo CultureInfo { get; }
    }
}
