﻿using ExemploAuth.Web.Data.Contextos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DataSqlServerExtensions
    {
        public static IServiceCollection AddEfCoreSqlServer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AuthContexto>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("AUTH_API"),
                    x => x.MigrationsAssembly("ExemploAuth.Web.Data.SqlServer"));
                options.EnableSensitiveDataLogging();
            });

            return services;
        }
    }
}