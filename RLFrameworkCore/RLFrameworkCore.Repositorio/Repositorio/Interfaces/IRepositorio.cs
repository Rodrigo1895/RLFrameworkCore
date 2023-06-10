using System.Linq.Expressions;

namespace RLFrameworkCore.Repositorio.Repositorio.Interfaces
{
    public interface IRepositorio<T> where T : class
    {
        Task<T> AdicionarAsync(T entidade, CancellationToken cancellationToken = default(CancellationToken));
        Task<IList<T>> AdicionarListaAsync(IList<T> entidades, CancellationToken cancellationToken = default(CancellationToken));
        Task<T> AtualizarAsync(T entidade, CancellationToken cancellationToken = default(CancellationToken), params Expression<Func<T, object>>[] propriedadesAtualizar);
        Task<bool> DeletarAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default(CancellationToken));
    }
}