using ExemploAuth.Web.Dominio.Entidades;
using RLFrameworkCore.Repositorio.Repositorio.Interfaces;

namespace ExemploAuth.Web.Dominio.Interfaces.Repositorios
{
    public interface IRefreshTokenRepositorio : IRepositorio<RefreshTokenEntidade>, IRepositorioLeitura<RefreshTokenEntidade>
    {
    }
}
