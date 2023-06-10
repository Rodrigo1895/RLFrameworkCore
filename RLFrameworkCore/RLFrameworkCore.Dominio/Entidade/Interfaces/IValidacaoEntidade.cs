using FluentValidation.Results;

namespace RLFrameworkCore.Dominio.Entidade.Interfaces
{
    public interface IValidacaoEntidade<T>
    {
        string NomeArquivo { get; }
        ValidationResult Validar(T obj);
    }
}
