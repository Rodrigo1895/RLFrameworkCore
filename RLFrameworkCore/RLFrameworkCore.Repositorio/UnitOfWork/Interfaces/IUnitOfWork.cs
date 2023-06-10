namespace RLFrameworkCore.Repositorio.UnitOfWork.Interfaces
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        Task CompleteAsync(CancellationToken cancellationToken = default);
        Task RollbackAsync(CancellationToken cancellationToken = default);
    }
}
