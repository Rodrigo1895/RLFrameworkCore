using RLFrameworkCore.Dominio.Entidade;
using RLFrameworkCore.Dominio.Entidade.Interfaces;

namespace ExemploAuth.Web.Dominio.Entidades
{
    public class UsuarioEntidade : EntidadeBase<UsuarioEntidade>
    {
        public int Id { get; private set; }
        public string Nome { get; private set; }
        public string Senha { get; private set; }

        public UsuarioEntidade(int id, string nome, string senha)
        {
            Id = id;
            Nome = nome;
            Senha = senha;
        }

        public override IList<IValidacaoEntidade<UsuarioEntidade>> GetValidacoes()
        {
            return null;
        }
    }
}
