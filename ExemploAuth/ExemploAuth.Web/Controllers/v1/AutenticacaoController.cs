using ExemploAuth.Web.Aplicacao.Servicos.Autenticacao;
using ExemploAuth.Web.Dto.RefreshToken;
using ExemploAuth.Web.Dto.Usuario;
using Microsoft.AspNetCore.Mvc;
using RLFrameworkCore.Dominio.Controller;
using RLFrameworkCore.Notificacao.Notificacao.Interfaces;

namespace ExemploAuth.Web.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/autenticacao")]
    public class AutenticacaoController : BaseController
    {
        private readonly IAutenticacaoAppServico _autenticacaoAppServico;

        public AutenticacaoController(INotificacaoContexto notificacaoContexto,
            IAutenticacaoAppServico autenticacaoAppServico) : base(notificacaoContexto)
        {
            _autenticacaoAppServico = autenticacaoAppServico; ;
        }

        [HttpPost]
        [Route("autenticar")]
        [ProducesResponseType(typeof(UsuarioAutenticadoDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Autenticar([FromBody] AutenticarUsuarioLoginDto dto)
        {
            return ResponsePostAuth(await _autenticacaoAppServico.AutenticarUsuarioLoginAsync(dto));
        }

        [HttpPost]
        [Route("refresh-token")]
        [ProducesResponseType(typeof(UsuarioAutenticadoDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Login([FromBody] AutenticarRefreshTokenDto dto)
        {
            return ResponsePostAuth(await _autenticacaoAppServico.AutenticarRefreshTokenAsync(dto));
        }
    }
}
