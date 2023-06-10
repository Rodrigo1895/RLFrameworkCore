using RLFrameworkCore.Dto.DTOs;
using System.Linq.Expressions;

namespace RLFrameworkCore.Repositorio.Repositorio.Interfaces
{ 
    public interface IRepositorioLeitura<T> where T : class
    {
        Task<T> BuscarAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default(CancellationToken));
        IQueryable<T> BuscarTodos(Expression<Func<T, bool>> func = null);
    }
}
