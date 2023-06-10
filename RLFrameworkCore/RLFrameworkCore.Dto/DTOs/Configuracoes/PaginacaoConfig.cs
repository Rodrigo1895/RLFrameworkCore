namespace RLFrameworkCore.Dto.DTOs.Configuracoes
{
    internal static class PaginacaoConfig
    {
        public static int PaginaTamanhoPadrao { get; private set; }
        public static int PaginaTamanhoMaximo { get; private set; }

        public static void Configurar(int paginaTamanhoPadrao, int paginaTamanhoMaximo)
        {
            PaginaTamanhoPadrao = paginaTamanhoPadrao;
            PaginaTamanhoMaximo = paginaTamanhoMaximo;
        }
    }
}
