using RLFrameworkCore.Dto.DTOs;

namespace Exemplo.Web.Dto.Cliente.Response
{
    public class ClienteDto : DtoBase
    {
        public int IdCliente { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Genero { get; set; }
    }
}
