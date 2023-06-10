using RLFrameworkCore.Dominio.Entidade.Interfaces;
using RLFrameworkCore.Dto.DTOs.Interfaces;
using RLFrameworkCore.Dto.DTOs.Validacao;
using RLFrameworkCore.Notificacao.Notificacao.Interfaces;
using RLFrameworkCore.Repositorio.UnitOfWork.Interfaces;

namespace RLFrameworkCore.Dominio.Servico
{
    public abstract class HandlerBase
    {
        protected HandlerBase(INotificacaoContexto notificacao)
        {
            Notificacao = notificacao;
        }
        public INotificacaoContexto Notificacao { get; }

        public async Task<bool> ValidarDto<TDto>(IValidacaoDtoBase validacaoDto, TDto dto) where TDto : IDto
        {
            return await ServicoUtils.ValidarDto(Notificacao, validacaoDto, dto);
        }

        public bool ValidarEntidade<T>(T entidade) where T : IEntidade<T>
        {
            return ServicoUtils.ValidarEntidade(Notificacao, entidade);
        }

        public async Task<bool> Commit(IUnitOfWork uow, CancellationToken cancellationToken = default)
        {
            return await ServicoUtils.Commit(Notificacao, uow, cancellationToken);
        }
    }
}
