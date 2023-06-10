using RLFrameworkCore.Dto.DTOs.Interfaces;
using RLFrameworkCore.Notificacao.Notificacao.Interfaces;

namespace RLFrameworkCore.Dto.DTOs.Validacao
{
    public interface IValidacaoDto<T> : IValidacaoDtoBase where T : IDto
    {
        void ValidarDto(T dto, INotificacaoContexto notificacao);
    }
}