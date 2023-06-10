using ExemploAuth.Web.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExemploAuth.Web.Data.Contextos.Mapeamentos.RefreshToken
{
    public class RefreshTokenMap : IEntityTypeConfiguration<RefreshTokenEntidade>
    {
        public void Configure(EntityTypeBuilder<RefreshTokenEntidade> builder)
        {
            builder.ToTable("refresh_token");

            builder.HasKey(k => k.UsuarioId);

            builder.Property(e => e.UsuarioId)
                .ValueGeneratedNever();

            builder.Property(p => p.Token).IsRequired();
        }
    }
}