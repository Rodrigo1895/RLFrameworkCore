using Exemplo.Web.Dominio.Entidades.Pedido;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Exemplo.Web.Data.Contextos.Mapeamentos.Pedido
{
    public class PedidoItemMap : IEntityTypeConfiguration<PedidoItemEntidade>
    {
        public void Configure(EntityTypeBuilder<PedidoItemEntidade> builder)
        {
            builder.ToTable("pedido_item");

            builder.HasKey(k => new { k.IdPedido, k.IdItem });
            builder.HasOne(p => p.Pedido)
               .WithMany(x => x.PedidoItens)
               .HasForeignKey(x => x.IdPedido);

            builder.Property(p => p.IdProduto).IsRequired();
            builder.HasOne(p => p.Produto)
                .WithMany()
                .HasForeignKey(x => x.IdProduto);

            builder.Property(p => p.Quantidade)
                .HasPrecision(12, 4);

            builder.Property(p => p.ValorUnit)
                .HasPrecision(12, 4);
        }
    }
}