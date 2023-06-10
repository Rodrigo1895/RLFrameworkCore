using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using RLFrameworkCore.Dto.DTOs.Interfaces;
using RLFrameworkCore.Notificacao.Notificacao.Interfaces;

namespace RLFrameworkCore.Dominio.Controller
{
    public class BaseController : ControllerBase
    {
        protected internal INotificacaoContexto NotificacaoContexto;

        protected BaseController(INotificacaoContexto notificacaoContexto)
        {
            NotificacaoContexto = notificacaoContexto;
        }

        [NonAction]
        protected IActionResult ResponseGet(IDto response)
        {
            if (response is null && !NotificacaoContexto.HasNotificacoes)
                return NotFound(null);

            return GetActionResult(Ok(response), $"Erro ao buscar.");
        }

        [NonAction]
        protected IActionResult ResponseGetAll<TDto>(IListDto<TDto> response)
        {
            int qtdItens = response?.Itens?.Count ?? 0;
            if (qtdItens <= 0 && !NotificacaoContexto.HasNotificacoes)
                return NotFound(null);

            return GetActionResult(Ok(response), $"Erro ao buscar.");
        }

        [NonAction]
        protected IActionResult ResponsePost(IDto response)
        {
            return GetActionResult(Created("", response), $"Erro ao criar.");
        }

        [NonAction]
        protected IActionResult ResponsePostAuth(IDto response)
        {
            return GetActionResult(Ok(response), $"Erro ao autenticar.");
        }

        [NonAction]
        protected IActionResult ResponsePut(IDto response)
        {
            return GetActionResult(Ok(response), $"Erro ao atualizar.");
        }

        [NonAction]
        protected IActionResult ResponseDelete(bool result)
        {
            if (!result && !NotificacaoContexto.HasNotificacoes)
                return BadRequest();
            
            return GetActionResult(Ok(result), $"Erro ao deletar.");
        }

        [NonAction]
        protected IActionResult ResponseGetFile(FileStream fileStream, string contentType)
        {
            if (fileStream is null && !NotificacaoContexto.HasNotificacoes)
                return NotFound(null);

            return GetActionResult(File(fileStream, contentType), $"Erro ao buscar arquivo");
        }

        [NonAction]
        private IActionResult GetActionResult(IActionResult actionResultDefault, string erro)
        {
            if (NotificacaoContexto.HasNotificacoes)
            {
                var erroResponse = new ValidationProblemDetails
                {
                    Title = erro,
                    Status = StatusCodes.Status400BadRequest
                };

                var notificacoesAgrupadas = NotificacaoContexto.Notificacoes.GroupBy(x => x.Campo).ToList();

                foreach(var n in notificacoesAgrupadas)
                {
                    erroResponse.Errors.Add(n.Key, n.Select(x => x.Mensagem).ToArray());
                }

                return BadRequest(erroResponse);
            }

            return actionResultDefault;
        }

        [NonAction]
        protected void AddErroModelState(ModelStateDictionary modelState)
        {
            var errors = modelState.Values.SelectMany(m => m.Errors);

            foreach (var error in errors)
            {
                var errorMessage = error.Exception == null ? error.ErrorMessage : error.Exception.Message;
                NotificacaoContexto.AddNotificacao(string.Empty, errorMessage);
            }
        }
    }
}
