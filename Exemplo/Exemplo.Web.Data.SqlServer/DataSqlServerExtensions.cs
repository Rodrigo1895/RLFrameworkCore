using Exemplo.Web.Data.Contextos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DataSqlServerExtensions
    {
        public static IServiceCollection AddEfCoreSqlServer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppContexto>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("EXEMPLO_API"),
                    x => x.MigrationsAssembly("Exemplo.Web.Data.SqlServer"));
                options.EnableSensitiveDataLogging();
            });

            return services;
        }
    }
}