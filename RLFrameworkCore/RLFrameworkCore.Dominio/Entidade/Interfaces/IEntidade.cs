namespace RLFrameworkCore.Dominio.Entidade.Interfaces
{
    public interface IEntidade<T> where T : IEntidade<T>
    {
        IList<IValidacaoEntidade<T>> GetValidacoes();
    }
}