using Exemplo.Web.Data.Contextos;
using Exemplo.Web.Dominio.Entidades.Cliente;
using Exemplo.Web.Dominio.Entidades.Pedido;
using Exemplo.Web.Dominio.Entidades.Produto;

namespace Exemplo.Web
{
    public static class SeedData
    {
        public static void AddSeedData(WebApplication app)
        {
            var random = new Random();

            var scope = app.Services.CreateScope();
            using (var context = scope.ServiceProvider.GetRequiredService<AppContexto>())
            {
                var pedidoJaIserido = context.Pedidos.FirstOrDefault(x => x.IdPedido == 1);

                if (pedidoJaIserido == null)
                {
                    #region Clientes

                    IList<ClienteEntidade> clientes = new List<ClienteEntidade>();
                    string genero;
                    for (int i = 1; i <= 10; i++)
                    {
                        if (i % 2 == 0)
                            genero = "M";
                        else
                            genero = "F";
                        clientes.Add(new ClienteEntidade(0, $"Cliente {i}", random.NextInt64(11111111111, 99999999999).ToString(), genero));
                    }
                    context.Clientes.AddRange(clientes);

                    #endregion

                    #region Produtos

                    IList<ProdutoEntidade> produtos = new List<ProdutoEntidade>();
                    for (int i = 1; i <= 10; i++)
                    {
                        produtos.Add(new ProdutoEntidade(0, $"Produto {i}", (decimal)(random.NextDouble() * (30 - 5) + 5)));
                    }
                    context.Produtos.AddRange(produtos);

                    #endregion

                    #region Pedidos

                    IList<PedidoEntidade> pedidos = new List<PedidoEntidade>();
                    IList<PedidoItemEntidade> itens = new List<PedidoItemEntidade>();
                    int idProduto;
                    int count = 1;
                    int idCliente = 1;
                    int qtdItens;
                    for (int i = 1; i <= 40; i++)
                    {
                        if (count > 10)
                        {
                            count = 1;
                            idCliente = 1;
                        }


                        var status = "A";
                        DateTime? dataHoraCoinclusao = null;
                        if (i % 2 == 1)
                        {
                            status = "C";
                            dataHoraCoinclusao = DateTime.Now;
                        }

                        pedidos.Add(new PedidoEntidade(0, count, status, DateTime.Now.AddDays(-count).AddMinutes(-10), dataHoraCoinclusao));

                        qtdItens = (i % 3) + 1;
                        for (int k = 1; k <= qtdItens; k++)
                        {
                            idProduto = random.Next(1, 10);
                            itens.Add(new PedidoItemEntidade(i, k, idProduto, k,
                                produtos.Where(x => x.IdProduto == idProduto).FirstOrDefault()?.Preco ?? 10));
                        }

                        count++;
                        idCliente++;
                    }

                    context.Pedidos.AddRange(pedidos);
                    context.SaveChanges();

                    context.PedidoItens.AddRange(itens);
                    context.SaveChanges();

                    #endregion
                }
            }
        }
    }
}
