using Exemplo.Web.Dominio.Entidades.Pedido;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Exemplo.Web.Data.Contextos.Mapeamentos.Pedido
{
    public class PedidoMap : IEntityTypeConfiguration<PedidoEntidade>
    {
        private string sequenceName = "seq_pedido";

        public PedidoMap(ModelBuilder modelBuilder)
        {
            modelBuilder.HasSequence<int>(sequenceName).StartsAt(1).IncrementsBy(1);
        }

        public void Configure(EntityTypeBuilder<PedidoEntidade> builder)
        {
            builder.ToTable("pedido");

            builder.HasKey(k => k.IdPedido);
            builder.Property(k => k.IdPedido).HasDefaultValueSql($"NEXT VALUE FOR {sequenceName}");

            builder.Property(p => p.IdCliente).IsRequired();
            builder.HasOne(p => p.Cliente)
                .WithMany()
                .HasForeignKey(x => x.IdCliente);

            builder.Property(p => p.Status)
                .HasMaxLength(1);

            builder.Property(p => p.DataHoraInclusao).IsRequired();
            builder.Property(p => p.DataHoraConclusao);
        }
    }
}