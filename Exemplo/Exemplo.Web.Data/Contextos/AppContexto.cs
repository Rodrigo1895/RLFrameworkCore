using Exemplo.Web.Data.Contextos.Mapeamentos.Cliente;
using Exemplo.Web.Data.Contextos.Mapeamentos.Pedido;
using Exemplo.Web.Data.Contextos.Mapeamentos.Produto;
using Exemplo.Web.Dominio.Entidades.Cliente;
using Exemplo.Web.Dominio.Entidades.Pedido;
using Exemplo.Web.Dominio.Entidades.Produto;
using Microsoft.EntityFrameworkCore;

namespace Exemplo.Web.Data.Contextos
{
    public class AppContexto : DbContext
    {
        public DbSet<ClienteEntidade> Clientes { get; set; }
        public DbSet<ProdutoEntidade> Produtos { get; set; }
        public DbSet<PedidoEntidade> Pedidos { get; set; }
        public DbSet<PedidoItemEntidade> PedidoItens { get; set; }

        public AppContexto(DbContextOptions<AppContexto> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Mapeamentos Entidades

            modelBuilder.ApplyConfiguration(new ClienteMap(modelBuilder));
            modelBuilder.ApplyConfiguration(new ProdutoMap(modelBuilder));
            modelBuilder.ApplyConfiguration(new PedidoMap(modelBuilder));
            modelBuilder.ApplyConfiguration(new PedidoItemMap());

            #endregion

            base.OnModelCreating(modelBuilder);
        }
    }
}