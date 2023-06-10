using ExemploAuth.Web.Data.Contextos;
using ExemploAuth.Web.Dominio.Entidades;
using ExemploAuth.Web.Dominio.Interfaces.Repositorios;
using RLFrameworkCore.Repositorio.Repositorio;

namespace ExemploAuth.Web.Data.Repositorios.Usuario
{
    public class UsuarioRepositorio : RepositorioBase<UsuarioEntidade>, IUsuarioRepositorio
    {
        public UsuarioRepositorio(AuthContexto context) : base(context)
        {
        }
    }
}