using Exemplo.Web.Dominio.Entidades.Cliente;
using Exemplo.Web.Dominio.Entidades.Pedido.Enums;
using Exemplo.Web.Dominio.Entidades.Pedido.Validacoes;
using RLFrameworkCore.Dominio.Entidade;
using RLFrameworkCore.Dominio.Entidade.Interfaces;

namespace Exemplo.Web.Dominio.Entidades.Pedido
{
    public class PedidoEntidade : EntidadeBase<PedidoEntidade>
    {
        #region Propriedades

        public int IdPedido { get; private set; }
        public int IdCliente { get; private set; }
        public string Status { get; private set; }
        public DateTime DataHoraInclusao { get; private set; }
        public DateTime? DataHoraConclusao { get; private set; }

        #endregion

        #region Navegação

        public ClienteEntidade Cliente { get; private set; }
        public IList<PedidoItemEntidade> PedidoItens { get; private set; }

        #endregion

        private PedidoEntidade() { }

        public PedidoEntidade(int idPedido, int idCliente, string status, DateTime dataHoraInclusao, DateTime? dataHoraConclusao)
        {
            IdPedido = idPedido;
            IdCliente = idCliente;
            Status = status;
            DataHoraInclusao = dataHoraInclusao;
            DataHoraConclusao = dataHoraConclusao;
        }

        public void ConcluirPedido()
        {
            Status = PedidoStatusEnum.C.ToString();
            DataHoraConclusao = DateTime.Now;
        }

        public override IList<IValidacaoEntidade<PedidoEntidade>> GetValidacoes()
        {
            return new List<IValidacaoEntidade<PedidoEntidade>>()
            {
                new PedidoValidacao()
            };
        }
    }
}