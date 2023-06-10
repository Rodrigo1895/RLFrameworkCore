using FluentValidation.Results;
using RLFrameworkCore.Dominio.Entidade.Interfaces;
using RLFrameworkCore.Dto.DTOs.Interfaces;
using RLFrameworkCore.Dto.DTOs.Validacao;
using RLFrameworkCore.Notificacao.Notificacao.Interfaces;
using RLFrameworkCore.Repositorio.UnitOfWork.Interfaces;

namespace RLFrameworkCore.Dominio.Servico
{
    internal static class ServicoUtils
    {
        public static async Task<bool> ValidarDto<TDto>(INotificacaoContexto notificacao, IValidacaoDtoBase validacaoDto, TDto dto) where TDto : IDto
        {
            if (validacaoDto.GetType().GetInterfaces().Contains(typeof(IValidacaoDtoFluent<TDto>)))
            {
                ValidationResult result = ((IValidacaoDtoFluent<TDto>)validacaoDto).ValidarDtoComFluent(dto);

                if (result != null && !result.IsValid)
                {
                    foreach (ValidationFailure erro in result.Errors)
                    {
                        notificacao.AddNotificacao(erro.PropertyName, erro.ErrorMessage, ((IValidacaoDtoFluent<TDto>)validacaoDto).NomeArquivo);
                    }
                }
            }

            if (validacaoDto.GetType().GetInterfaces().Contains(typeof(IValidacaoDto<TDto>)))
            {
                ((IValidacaoDto<TDto>)validacaoDto).ValidarDto(dto, notificacao);
            }

            if (validacaoDto.GetType().GetInterfaces().Contains(typeof(IValidacaoDtoAsync<TDto>)))
            {
                await ((IValidacaoDtoAsync<TDto>)validacaoDto).ValidarDtoAsync(dto, notificacao);
            }

            return !notificacao.HasNotificacoes;
        }

        public static bool ValidarEntidade<T>(INotificacaoContexto notificacao, T entidade) where T : IEntidade<T>
        {
            bool valido = true;

            IList<IValidacaoEntidade<T>> validacoes = entidade?.GetValidacoes();

            if (validacoes != null && validacoes.Count > 0)
            {
                foreach (IValidacaoEntidade<T> val in validacoes)
                {
                    ValidationResult ResultadosValidacao = val.Validar(entidade);
                    if (!ResultadosValidacao.IsValid)
                    {
                        valido = false;
                        foreach (ValidationFailure erro in ResultadosValidacao.Errors)
                        {
                            if (string.IsNullOrWhiteSpace(val.NomeArquivo))
                                notificacao.AddNotificacao(erro.PropertyName, erro.ErrorMessage);
                            else
                                notificacao.AddNotificacao(erro.PropertyName, erro.ErrorMessage, val.NomeArquivo);
                        }
                    }
                }
            }

            return valido;
        }

        public static async Task<bool> Commit(INotificacaoContexto notificacao, IUnitOfWork uow, CancellationToken cancellationToken = default)
        {
            if (!notificacao.HasNotificacoes)
            {
                await uow.CompleteAsync(cancellationToken);
                return true;
            }
            else
            {
                await uow.RollbackAsync(cancellationToken);
                return false;
            }
        }
    }
}
