using Exemplo.Web.Dominio.Entidades.Pedido.Enums;
using Exemplo.Web.Dominio.Localizacao;
using FluentValidation;
using RLFrameworkCore.Dominio.Entidade;

namespace Exemplo.Web.Dominio.Entidades.Pedido.Validacoes
{
    public class PedidoValidacao : ValidacaoEntidade<PedidoEntidade>
    {
        public override string NomeArquivo { get; protected set; } = Constants.NomeErroSource;

        public PedidoValidacao()
        {

            RuleFor(a => a.Status)
                .Must(x => Enum.IsDefined(typeof(PedidoStatusEnum), x))
                .WithMessage(EnumMensagensErro.PedidoStatusInvalido.ToString());
        }
    }
}
