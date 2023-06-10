using RLFrameworkCore.Dominio.Entidade.Interfaces;

namespace RLFrameworkCore.Dominio.Entidade
{
    public abstract class EntidadeBase<T> : IEntidade<T> where T : EntidadeBase<T>
    {
        public abstract IList<IValidacaoEntidade<T>> GetValidacoes();
    }
}