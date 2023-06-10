using RLFrameworkCore.Notificacao.Notificacao.Interfaces;

namespace RLFrameworkCore.Notificacao.Notificacao.Implementacoes
{
    internal class NotificacaoModel : INotificacaoModel
    {
        public NotificacaoModel(string campo, string mensagem)
        {
            Campo = campo;
            Mensagem = mensagem;
        }

        public NotificacaoModel(string mensagem)
        {
            Mensagem = mensagem;
        }

        public string Campo { get; }

        public string Mensagem { get; }
    }
}
