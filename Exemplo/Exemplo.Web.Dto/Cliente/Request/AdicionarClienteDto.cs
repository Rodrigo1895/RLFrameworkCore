using RLFrameworkCore.Dto.DTOs;

namespace Exemplo.Web.Dto.Cliente.Request
{
    public class AdicionarClienteDto : DtoBase
    {
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Genero { get; set; }
    }
}
