using ExemploAuth.Web.Data.Contextos.Mapeamentos.RefreshToken;
using ExemploAuth.Web.Data.Contextos.Mapeamentos.Usuario;
using ExemploAuth.Web.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;

namespace ExemploAuth.Web.Data.Contextos
{
    public class AuthContexto : DbContext
    {
        public DbSet<RefreshTokenEntidade> RefreshToken { get; set; }
        public DbSet<UsuarioEntidade> Usuario { get; set; }

        public AuthContexto(DbContextOptions<AuthContexto> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new RefreshTokenMap());
            modelBuilder.ApplyConfiguration(new UsuarioMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}