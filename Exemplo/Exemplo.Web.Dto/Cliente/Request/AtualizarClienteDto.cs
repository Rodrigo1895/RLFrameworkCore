using RLFrameworkCore.Dto.DTOs;

namespace Exemplo.Web.Dto.Cliente.Request
{
    public class AtualizarClienteDto : DtoBase
    {
        public string Nome { get; set; }
        public string Genero { get; set; }
    }
}
