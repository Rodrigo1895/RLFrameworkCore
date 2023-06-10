using RLFrameworkCore.Dto.DTOs.Interfaces;
using RLFrameworkCore.Notificacao.Notificacao.Interfaces;

namespace RLFrameworkCore.Dto.DTOs.Validacao
{
    public interface IValidacaoDtoAsync<T> : IValidacaoDtoBase where T : IDto
    {
        Task ValidarDtoAsync(T dto, INotificacaoContexto notificacao);
    }
}
