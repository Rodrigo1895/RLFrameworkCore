using Microsoft.AspNetCore.Mvc;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class VersionamentoExtension
    {
        public static IServiceCollection AddVersionamentoApiConfiguracao(this IServiceCollection services)
        {
            services.AddApiVersioning(config =>
            {
                config.DefaultApiVersion = new ApiVersion(1, 0);
                config.AssumeDefaultVersionWhenUnspecified = true;
                config.ReportApiVersions = true;
            });

            services.AddVersionedApiExplorer(p =>
            {
                p.GroupNameFormat = "'v'VVV";
                p.SubstituteApiVersionInUrl = true;
            });

            return services;
        }

        public static IApplicationBuilder UseVersionamentoApiConfiguracao(this IApplicationBuilder app)
        {
            app.UseApiVersioning();

            return app;
        }
    }
}
