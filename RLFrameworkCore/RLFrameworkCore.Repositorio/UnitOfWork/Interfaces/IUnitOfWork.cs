namespace RLFrameworkCore.Repositorio.UnitOfWork.Interfaces
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        Task CompleteAsync();
        Task RollbackAsync();
    }
}
