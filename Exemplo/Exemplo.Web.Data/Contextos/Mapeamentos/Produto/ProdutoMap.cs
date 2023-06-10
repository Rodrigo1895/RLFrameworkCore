using Exemplo.Web.Dominio.Entidades.Produto;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Exemplo.Web.Data.Contextos.Mapeamentos.Produto
{
    public class ProdutoMap : IEntityTypeConfiguration<ProdutoEntidade>
    {
        private string sequenceName = "seq_produto";

        public ProdutoMap(ModelBuilder modelBuilder)
        {
            modelBuilder.HasSequence<int>(sequenceName).StartsAt(1).IncrementsBy(1);
        }

        public void Configure(EntityTypeBuilder<ProdutoEntidade> builder)
        {
            builder.ToTable("produto");

            builder.HasKey(k => k.IdProduto);
            builder.Property(k => k.IdProduto).HasDefaultValueSql($"NEXT VALUE FOR {sequenceName}");

            builder.Property(p => p.Descricao)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(p => p.Preco)
                .HasPrecision(12, 4);
        }
    }
}