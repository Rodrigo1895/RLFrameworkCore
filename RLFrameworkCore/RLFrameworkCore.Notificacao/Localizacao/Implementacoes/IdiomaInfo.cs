using RLFrameworkCore.Notificacao.Localizacao.Interfaces;
using System.Globalization;

namespace RLFrameworkCore.Notificacao.Localizacao.Implementacoes
{
    internal class IdiomaInfo : IIdiomaInfo
    {
        public IdiomaInfo(string nome, bool padrao = false)
        {
            Nome = nome;
            Padrao = padrao;
            CultureInfo = new CultureInfo(nome);
        }

        public string Nome { get; }
        public bool Padrao { get; }
        public CultureInfo CultureInfo { get; }
    }
}
