namespace RLFrameworkCore.Dto.DTOs.Interfaces
{ 
    public interface IListDto<TDto>
    {
        bool TemProximo { get; set; }
        int PaginaAtual { get; set; }
        int PaginaTamanho { get; set; }
        int TotalItens { get; set; }
        int TotalPaginas { get; set; }
        IList<TDto> Itens { get; set; }
    }
}
