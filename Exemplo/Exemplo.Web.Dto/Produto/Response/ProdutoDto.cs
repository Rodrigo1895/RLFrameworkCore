using RLFrameworkCore.Dto.DTOs;

namespace Exemplo.Web.Dto.Produto.Response
{
    public class ProdutoDto : DtoBase
    {
        public int IdProduto { get; set; }
        public string Descricao { get; set; }
    }
}
