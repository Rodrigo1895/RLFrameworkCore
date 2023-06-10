using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using RLFrameworkCore.Repositorio.UnitOfWork.Interfaces;

namespace RLFrameworkCore.Repositorio.UnitOfWork
{
    internal class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _contexto;
        private readonly IDbContextTransaction _transacao;
        public UnitOfWork(DbContext contexto)
        {
            _contexto = contexto;
            _transacao = _contexto.Database.BeginTransaction();
        }
        public async Task CompleteAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                await _contexto.SaveChangesAsync(cancellationToken);
                await _transacao.CommitAsync(cancellationToken);
                await DisposeAsync();
            }
            catch (DbUpdateException ex)
            {
                await RollbackAsync(cancellationToken);
                throw new DbUpdateException($"Erro ao efetuar commit de transação  - [UnitOfWork.CompleteAsync]", ex);
            }
        }

        public async Task RollbackAsync(CancellationToken cancellationToken = default)
        {
            await _transacao.RollbackAsync(cancellationToken);
            await DisposeAsync();
        }

        public async ValueTask DisposeAsync()
        {
            await _transacao.DisposeAsync();
        }
    }
}