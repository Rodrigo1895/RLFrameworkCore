using FluentValidation;
using FluentValidation.Results;
using RLFrameworkCore.Dominio.Entidade.Interfaces;

namespace RLFrameworkCore.Dominio.Entidade
{
    public abstract class ValidacaoEntidade<T> : AbstractValidator<T>, IValidacaoEntidade<T>
    {
        public abstract string NomeArquivo { get; protected set; }

        public ValidationResult Validar(T obj) 
        {
            return Validate(obj);
        }
    }
}
