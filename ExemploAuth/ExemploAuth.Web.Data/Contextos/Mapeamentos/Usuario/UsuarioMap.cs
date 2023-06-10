using ExemploAuth.Web.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExemploAuth.Web.Data.Contextos.Mapeamentos.Usuario
{
    public class UsuarioMap : IEntityTypeConfiguration<UsuarioEntidade>
    {
        public void Configure(EntityTypeBuilder<UsuarioEntidade> builder)
        {
            builder.ToTable("usuario");

            builder.HasKey(k => k.Id);

            builder.Property(e => e.Id)
                .ValueGeneratedNever();

            builder.Property(p => p.Nome).IsRequired();

            builder.Property(p => p.Senha).IsRequired();
        }
    }
}