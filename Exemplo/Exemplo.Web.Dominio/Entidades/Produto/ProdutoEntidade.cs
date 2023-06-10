using RLFrameworkCore.Dominio.Entidade;
using RLFrameworkCore.Dominio.Entidade.Interfaces;

namespace Exemplo.Web.Dominio.Entidades.Produto
{
    public class ProdutoEntidade : EntidadeBase<ProdutoEntidade>
    {
        #region Propriedades

        public int IdProduto { get; private set; }
        public string Descricao { get; private set; }
        public decimal Preco { get; private set; }

        #endregion

        public ProdutoEntidade(int idProduto, string descricao, decimal preco)
        {
            IdProduto = idProduto;
            Descricao = descricao;
            Preco = preco;
        }

        public override IList<IValidacaoEntidade<ProdutoEntidade>> GetValidacoes()
        {
            return null;
        }
    }
}