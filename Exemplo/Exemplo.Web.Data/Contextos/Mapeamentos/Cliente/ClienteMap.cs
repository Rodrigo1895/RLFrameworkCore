using Exemplo.Web.Dominio.Entidades.Cliente;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Exemplo.Web.Data.Contextos.Mapeamentos.Cliente
{
    public class ClienteMap : IEntityTypeConfiguration<ClienteEntidade>
    {
        private readonly string sequenceName = "seq_cliente";

        public ClienteMap(ModelBuilder modelBuilder)
        {
            modelBuilder.HasSequence<int>(sequenceName).StartsAt(1).IncrementsBy(1);
        }

        public void Configure(EntityTypeBuilder<ClienteEntidade> builder)
        {
            builder.ToTable("cliente");

            builder.HasKey(k => k.IdCliente);
            builder.Property(k => k.IdCliente).HasDefaultValueSql($"NEXT VALUE FOR {sequenceName}");

            builder.Property(p => p.Nome)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(p => p.CPF)
                .HasMaxLength(11)
                .IsRequired();
            builder.HasIndex(p => p.CPF).IsUnique();

            builder.Property(p => p.Genero)
                .HasMaxLength(1)
                .IsRequired();
        }
    }
}