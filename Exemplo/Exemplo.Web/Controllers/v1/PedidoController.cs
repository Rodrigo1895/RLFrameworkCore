using Exemplo.Web.Aplicacao.Servicos.Pedido;
using Exemplo.Web.Dto.Pedido.Request;
using Exemplo.Web.Dto.Pedido.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RLFrameworkCore.Dominio.Controller;
using RLFrameworkCore.Dto.DTOs.Interfaces;
using RLFrameworkCore.Notificacao.Notificacao.Interfaces;

namespace Exemplo.Web.Controllers.v1
{
    [ApiController]
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/pedido")]
    public class PedidoController : BaseController
    {
        private readonly IPedidoAppServico _pedidoAppServico;

        public PedidoController(INotificacaoContexto notificacaoContexto,
            IPedidoAppServico pedidoAppServico) : base(notificacaoContexto)
        {
            _pedidoAppServico = pedidoAppServico;
        }

        [HttpPost]
        [ProducesResponseType(typeof(PedidoDto), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(typeof(ValidationProblemDetails), 400)]
        public async Task<IActionResult> Adicionar([FromBody] AdicionarPedidoDto dto)
        {
            var response = await _pedidoAppServico.AdicionarPedido(dto);

            return ResponsePost(response);
        }

        [HttpPut("concluir/{idPedido}")]
        [ProducesResponseType(typeof(PedidoDto), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(typeof(ValidationProblemDetails), 400)]
        public async Task<IActionResult> ConcluirPedido([FromRoute] int idPedido)
        {
            var response = await _pedidoAppServico.ConcluirPedido(idPedido);

            return ResponsePut(response);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(PedidoDto), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(typeof(ValidationProblemDetails), 400)]
        public async Task<IActionResult> BuscarPorId(int id)
        {
            var response = await _pedidoAppServico.BuscarPorId(id);

            return ResponseGet(response);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IListDto<PedidoDto>), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(typeof(ValidationProblemDetails), 400)]
        public async Task<IActionResult> Buscar([FromQuery] BuscarPedidosDto dto)
        {
            var response = await _pedidoAppServico.Buscar(dto);

            return ResponseGetAll(response);
        }
    }
}