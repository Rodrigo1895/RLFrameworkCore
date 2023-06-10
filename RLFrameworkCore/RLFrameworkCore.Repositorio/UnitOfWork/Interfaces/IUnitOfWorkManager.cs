using Microsoft.EntityFrameworkCore;

namespace RLFrameworkCore.Repositorio.UnitOfWork.Interfaces
{
    public interface IUnitOfWorkManager<TDbContext> where TDbContext : DbContext
    {
        IUnitOfWork Begin();
    }
}
