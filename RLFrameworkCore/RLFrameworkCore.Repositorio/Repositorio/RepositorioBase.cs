using Microsoft.EntityFrameworkCore;
using RLFrameworkCore.Repositorio.Repositorio.Interfaces;
using System.Linq.Expressions;

namespace RLFrameworkCore.Repositorio.Repositorio
{
    public class RepositorioBase<T> : IRepositorio<T>, IRepositorioLeitura<T> where T : class
    {
        protected DbContext _context;
        protected DbSet<T> _dbSet;
        public RepositorioBase(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<T> AdicionarAsync(T entidade, CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                await _dbSet.AddAsync(entidade, cancellationToken);
                await SaveChangesAsync(cancellationToken);

                return entidade;
            }
            catch (DbUpdateException ex)
            {
                throw new DbUpdateException($"Erro ao inserir {typeof(T).Name} - [RepositorioBase.AdicionarAsync]", ex);
            }
        }

        public async Task<IList<T>> AdicionarListaAsync(IList<T> entidades, CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                await _dbSet.AddRangeAsync(entidades, cancellationToken);
                await SaveChangesAsync(cancellationToken);

                return entidades;
            }
            catch (DbUpdateException ex)
            {
                throw new DbUpdateException($"Erro ao inserir {typeof(T).Name} - [RepositorioBase.AdicionarAsync]", ex);
            }
        }

        public async Task<T> AtualizarAsync(T entidade, CancellationToken cancellationToken = default(CancellationToken), params Expression<Func<T, object>>[] propriedadesAtualizar)
        {
            try
            {
                var dbEntityEntry = _context.Entry(entidade);
                if (propriedadesAtualizar.Any())
                {
                    foreach (var propriedade in propriedadesAtualizar)
                    {
                        dbEntityEntry.Property(propriedade).IsModified = true;
                    }
                }
                else
                {
                    _dbSet.Update(entidade);
                }

                await SaveChangesAsync(cancellationToken);

                return entidade;
            }
            catch (DbUpdateException ex)
            {
                throw new DbUpdateException($"Erro ao atualizar {typeof(T).Name} - [RepositorioBase.AtualizarAsync]", ex);
            }
        }

        public async Task<bool> DeletarAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                foreach (T item in _dbSet.Where(predicate).ToList())
                {
                    _dbSet.Remove(item);
                }

                await SaveChangesAsync(cancellationToken);

                return true;
            }
            catch (DbUpdateException ex)
            {
                throw new DbUpdateException($"Erro ao deletar {typeof(T).Name} - [RepositorioBase.DeletarAsync]", ex);
            }
        }

        public async Task<T> BuscarAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                return await _dbSet.AsNoTracking().FirstOrDefaultAsync(predicate, cancellationToken);
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException($"Erro ao buscar por ID {typeof(T).Name} - [RepositorioBase.BuscarPorIdAsync]", ex);
            }
        }

        public IQueryable<T> BuscarTodos(Expression<Func<T, bool>> func = null)
        {
            try
            {
                var queryable = _dbSet.AsNoTracking();

                if (func != null)
                    queryable = queryable.Where(func);

                return queryable;
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException($"Erro ao buscar {typeof(T).Name} - [RepositorioBase.BuscarTodosAsync]", ex);
            }
        }

        private async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        /*private Expression<Func<T, bool>> RetornaWhereChavePrimaria(T entidade)
        {
            // Busca campos da chave primária da entidade
            IEnumerable<string> propriedadesChavePrimaria = _context.Model.FindEntityType(typeof(T)).FindPrimaryKey().Properties.Select(x => x.Name);

            var parameter = Expression.Parameter(typeof(T), "x");
            BinaryExpression exppressionFinal = null;
            foreach (var p in propriedadesChavePrimaria)
            {
                // busca valor do campo da chave primária
                var valor = entidade.GetType().GetProperty(p).GetValue(entidade, null);

                var binaryExpression = Expression.Equal(
                        Expression.Property(parameter, p),
                        Expression.Constant(valor));

                if (exppressionFinal != null)
                    exppressionFinal = Expression.AndAlso(exppressionFinal, binaryExpression);
                else
                    exppressionFinal = binaryExpression;
            }

            return Expression.Lambda<Func<T, bool>>(exppressionFinal, parameter);
        }*/
    }
}
