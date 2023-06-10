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
        public async Task CompleteAsync()
        {
            try
            {
                await _contexto.SaveChangesAsync();
                await _transacao.CommitAsync();
                await DisposeAsync();
            }
            catch (DbUpdateException ex)
            {
                await RollbackAsync();
                throw new DbUpdateException($"Erro ao efetuar commit de transação  - [UnitOfWork.CompleteAsync]", ex);
            }
        }

        public async Task RollbackAsync()
        {
            await _transacao.RollbackAsync();
            await DisposeAsync();
        }

        public async ValueTask DisposeAsync()
        {
            await _transacao.DisposeAsync();
        }
    }
}