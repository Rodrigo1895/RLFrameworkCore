namespace RLFrameworkCore.Notificacao.Notificacao.Interfaces
{
    public interface INotificacaoContexto
    {
        IReadOnlyCollection<INotificacaoModel> Notificacoes { get; }
        bool HasNotificacoes { get; }
        void AddNotificacao(string chave, string enumString, string nomeArquivo);
        void AddNotificacao(string chave, Enum @enum, string nomeArquivo);
        void AddNotificacao(string chave, string mensagem);
    }
}
