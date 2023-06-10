using RLFrameworkCore.Dto.DTOs;
using Swashbuckle.AspNetCore.Annotations;

namespace Exemplo.Web.Dto.Pedido.Request
{
    public class BuscarPedidosDto : RequestAllDto
    {
        public BuscarPedidosDto()
        {
        }

        [SwaggerParameter("Inclui informações do cliente no retorno")]
        public bool RetornaCliente { get; set; }
        
        [SwaggerParameter("Inclui informações dos itens no retorno")]
        public bool RetornaItens { get; set; }
        
        public int? Id { get; set; }

        [SwaggerParameter("Formato suportado: DD-MM-YYYY")]
        public DateTime? DataInicial { get; set; }

        [SwaggerParameter("Formato suportado: DD-MM-YYYY")]
        public DateTime? DataFinal { get; set; }

        [SwaggerParameter(@"A - Aberto <br/>
                            C - Concluído")]
        public string Status { get; set; }
    }
}