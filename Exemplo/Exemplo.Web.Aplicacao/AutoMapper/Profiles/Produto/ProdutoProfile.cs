using AutoMapper;
using Exemplo.Web.Dominio.Entidades.Produto;
using Exemplo.Web.Dto.Produto.Response;

namespace Exemplo.Web.Aplicacao.AutoMapper.Profiles.Produto
{
    public class ProdutoProfile : Profile
    {
        public ProdutoProfile()
        {
            CreateMap<ProdutoEntidade, ProdutoDto>();
        }
    }
}
