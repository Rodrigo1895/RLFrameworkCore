using Exemplo.Web.Aplicacao.Servicos.Cliente;
using Exemplo.Web.Dto.Cliente.Request;
using Exemplo.Web.Dto.Cliente.Response;
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
    [Route("api/v{version:apiVersion}/cliente")]
    public class ClienteController : BaseController
    {
        private readonly IClienteAppServico _clienteAppServico;

        public ClienteController(INotificacaoContexto notificacaoContexto,
            IClienteAppServico clienteAppServico) : base(notificacaoContexto)
        {
            _clienteAppServico = clienteAppServico;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ClienteDto), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(typeof(ValidationProblemDetails), 400)]
        public async Task<IActionResult> Adicionar([FromBody] AdicionarClienteDto dto)
        {
            var response = await _clienteAppServico.Adicionar(dto);

            return ResponsePost(response);
        }

        [HttpPut("{idCliente}")]
        [ProducesResponseType(typeof(ClienteDto), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(typeof(ValidationProblemDetails), 400)]
        public async Task<IActionResult> Atualizar([FromRoute] int idCliente, [FromBody] AtualizarClienteDto dto)
        {
            var response = await _clienteAppServico.Atualizar(idCliente, dto);

            return ResponsePut(response);
        }

        [HttpDelete("{idCliente}")]
        [ProducesResponseType(typeof(bool), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(typeof(ValidationProblemDetails), 400)]
        public async Task<IActionResult> Deletar([FromRoute] int idCliente)
        {
            var response = await _clienteAppServico.Deletar(idCliente);

            return ResponseDelete(response);
        }

        [HttpGet("{idCliente}")]
        [ProducesResponseType(typeof(ClienteDto), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(typeof(ValidationProblemDetails), 400)]
        public async Task<IActionResult> BuscarPorId([FromRoute] int idCliente)
        {
            var response = await _clienteAppServico.BuscarPorId(idCliente);

            return ResponseGet(response);
        }

        [HttpGet("cpf/{cpf}")]
        [ProducesResponseType(typeof(ClienteDto), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(typeof(ValidationProblemDetails), 400)]
        public async Task<IActionResult> BuscarPorId([FromRoute] string cpf)
        {
            var response = await _clienteAppServico.BuscarPorCpf(cpf);

            return ResponseGet(response);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IListDto<ClienteDto>), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(typeof(ValidationProblemDetails), 400)]
        public async Task<IActionResult> Buscar([FromQuery] BuscarClientesDto dto)
        {
            var response = await _clienteAppServico.Buscar(dto);

            return ResponseGetAll(response);
        }
    }
}
