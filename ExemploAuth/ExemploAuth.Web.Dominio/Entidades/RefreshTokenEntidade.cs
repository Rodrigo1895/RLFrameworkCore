using RLFrameworkCore.Dominio.Entidade;
using RLFrameworkCore.Dominio.Entidade.Interfaces;

namespace ExemploAuth.Web.Dominio.Entidades
{
    public class RefreshTokenEntidade : EntidadeBase<RefreshTokenEntidade>
    {
        public int UsuarioId { get; private set; }
        public string Token { get; private set; }
        public DateTime DataExpiracao { get; private set; }

        public RefreshTokenEntidade(int usuarioId, string token, DateTime dataExpiracao)
        {
            UsuarioId = usuarioId;
            Token = token;
            DataExpiracao = dataExpiracao;
        }

        public override IList<IValidacaoEntidade<RefreshTokenEntidade>> GetValidacoes()
        {
            return null;
        }
    }
}