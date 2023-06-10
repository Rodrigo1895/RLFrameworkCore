using Exemplo.Web.Dominio.Entidades.Produto;
using RLFrameworkCore.Dominio.Entidade;
using RLFrameworkCore.Dominio.Entidade.Interfaces;

namespace Exemplo.Web.Dominio.Entidades.Pedido
{
    public  class PedidoItemEntidade : EntidadeBase<PedidoItemEntidade>
    {
        #region Propriedades

        public int IdPedido { get; private set; }
        public int IdItem { get; private set; }
        public int IdProduto { get; private set; }
        public decimal Quantidade { get; private set; }
        public decimal ValorUnit { get; private set; }

        #endregion

        #region Navegação

        public PedidoEntidade Pedido { get; private set; }
        public ProdutoEntidade Produto { get; private set; }

        #endregion

        private PedidoItemEntidade() { }

        public PedidoItemEntidade(int idPedido, int idItem, int idProduto, decimal quantidade, decimal valorUnit)
        {
            IdPedido = idPedido;
            IdItem = idItem;
            IdProduto = idProduto;
            Quantidade = quantidade;
            ValorUnit = valorUnit;
        }

        public override IList<IValidacaoEntidade<PedidoItemEntidade>> GetValidacoes()
        {
            return null;
        }
    }
}