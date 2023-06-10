using ExemploAuth.Web.Data.Repositorios.RefreshToken;
using ExemploAuth.Web.Data.Repositorios.Usuario;
using ExemploAuth.Web.Dominio.Interfaces.Repositorios;

namespace Microsoft.Extensions.DependencyInjection
{
    internal static class RepositoriosExtensions
    {
        internal static IServiceCollection RegistraRepositorios(this IServiceCollection services)
        {
            services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
            services.AddScoped<IRefreshTokenRepositorio, RefreshTokenRepositorio>();

            return services;
        }
    }
}
