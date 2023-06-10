using Microsoft.EntityFrameworkCore;
using RLFrameworkCore.Repositorio.UnitOfWork.Interfaces;

namespace RLFrameworkCore.Repositorio.UnitOfWork
{
    internal class UnitOfWorkManager<TDbContext> : IUnitOfWorkManager<TDbContext> where TDbContext : DbContext
    {
        private readonly TDbContext _contexto;

        public UnitOfWorkManager(TDbContext contexto)
        {
            _contexto = contexto;
        }

        public IUnitOfWork Begin()
        {
            return new UnitOfWork(_contexto);
        }
    }
}
