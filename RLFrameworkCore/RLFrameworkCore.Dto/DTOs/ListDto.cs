using RLFrameworkCore.Dto.DTOs.Interfaces;

namespace RLFrameworkCore.Dto.DTOs
{
    public class ListDto<TDto> : IListDto<TDto>
    {
        public bool TemProximo { get; set; }
        public int PaginaAtual { get; set; }
        public int PaginaTamanho { get; set; }        
        public int TotalItens { get; set; }
        public int TotalPaginas { get; set; }
        public IList<TDto> Itens { get; set; }
    }
}
