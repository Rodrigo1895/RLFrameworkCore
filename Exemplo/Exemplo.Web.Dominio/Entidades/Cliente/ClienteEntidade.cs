using Exemplo.Web.Dominio.Entidades.Cliente.Validacoes;
using RLFrameworkCore.Dominio.Entidade;
using RLFrameworkCore.Dominio.Entidade.Interfaces;

namespace Exemplo.Web.Dominio.Entidades.Cliente
{
    public class ClienteEntidade : EntidadeBase<ClienteEntidade>
    {
        #region Propriedades

        public int IdCliente { get; private set; }
        public string Nome { get; private set; }
        public string CPF { get; private set; }
        public string Genero { get; private set; }

        #endregion

        private ClienteEntidade() { }

        public ClienteEntidade(int idCliente, string nome, string cPF, string genero)
        {
            IdCliente = idCliente;
            Nome = nome;
            CPF = cPF;
            Genero = genero;    
        }

        public override IList<IValidacaoEntidade<ClienteEntidade>> GetValidacoes()
        {
            return new List<IValidacaoEntidade<ClienteEntidade>>()
            {
                new ClienteValidacao()
            };
        }
    }
}