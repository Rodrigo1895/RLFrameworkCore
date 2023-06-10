using FluentValidation.Results;
using RLFrameworkCore.Dto.DTOs.Interfaces;

namespace RLFrameworkCore.Dto.DTOs.Validacao
{
    public interface IValidacaoDtoFluent<T> : IValidacaoDtoBase where T : IDto
    {
        string NomeArquivo { get; }
        ValidationResult ValidarDtoComFluent(T dto);
    }
}