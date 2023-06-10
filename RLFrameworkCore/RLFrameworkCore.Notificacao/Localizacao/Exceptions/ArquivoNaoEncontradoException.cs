namespace RLFrameworkCore.Notificacao.Localizacao.Exceptions
{
    internal class ArquivoNaoEncontradoException : Exception
    {
        public ArquivoNaoEncontradoException()
        {
        }

        public ArquivoNaoEncontradoException(string message)
            : base(message)
        {
        }

        public ArquivoNaoEncontradoException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
