using Microsoft.EntityFrameworkCore;
using RLFrameworkCore.Repositorio.UnitOfWork;
using RLFrameworkCore.Repositorio.UnitOfWork.Interfaces;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class UnitOfWorkExtension
    {
        public static IServiceCollection AddUnitOfWorkRepositorio<TDbContext>(this IServiceCollection services) where TDbContext : DbContext
        {            
            services.AddTransient<IUnitOfWorkManager<TDbContext>, UnitOfWorkManager<TDbContext>>();

            return services;
        }
    }
}