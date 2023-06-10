using Exemplo.Web.Dominio.Entidades.Cliente.Enums;
using Exemplo.Web.Dominio.Localizacao;
using FluentValidation;
using RLFrameworkCore.Dominio.Entidade;

namespace Exemplo.Web.Dominio.Entidades.Cliente.Validacoes
{
    public class ClienteValidacao : ValidacaoEntidade<ClienteEntidade>
    {
        public override string NomeArquivo { get; protected set; } = Constants.NomeErroSource;

        public ClienteValidacao()
        {
            RuleFor(a => a.Nome)
                .NotEmpty()
                .WithMessage(EnumMensagensErro.NomeClienteObrigatorio.ToString());

            RuleFor(a => a.Genero)
                .Must(x => Enum.IsDefined(typeof(GeneroEnum), x))
                .WithMessage(EnumMensagensErro.GeneroInvalido.ToString());
        }
    }
}
    