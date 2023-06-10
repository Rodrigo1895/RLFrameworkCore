namespace RLFrameworkCore.Dto.DTOs.Interfaces
{
    public interface IRequestAllDto : IDto
    {
        int Pagina { get; set; }
        int PaginaTamanho { get; set; }
    }
}
