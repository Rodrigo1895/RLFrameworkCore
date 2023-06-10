using ExemploAuth.Web.Data.Contextos;
using ExemploAuth.Web.Dominio.Entidades;
using ExemploAuth.Web.Dominio.Interfaces.Repositorios;
using RLFrameworkCore.Repositorio.Repositorio;

namespace ExemploAuth.Web.Data.Repositorios.RefreshToken
{
    public class RefreshTokenRepositorio : RepositorioBase<RefreshTokenEntidade>, IRefreshTokenRepositorio
    {
        public RefreshTokenRepositorio(AuthContexto context) : base(context)
        {
        }
    }
}