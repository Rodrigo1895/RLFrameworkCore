using Exemplo.Web.Dominio;
using Exemplo.Web.Dominio.Entidades.Pedido.Enums;
using Exemplo.Web.Dominio.Localizacao;
using Exemplo.Web.Dto.Pedido.Request;
using FluentValidation;
using FluentValidation.Results;
using RLFrameworkCore.Dto.DTOs.Validacao;

namespace Exemplo.Web.Aplicacao.Servicos.Pedido.Validacoes
{
    public class ValidarBuscarPedidosDto : AbstractValidator<BuscarPedidosDto>, IValidacaoDtoFluent<BuscarPedidosDto>
    {
        public string NomeArquivo => Constants.NomeErroSource;

        public ValidationResult ValidarDtoComFluent(BuscarPedidosDto dto)
        {
            if (!string.IsNullOrEmpty(dto.Status))
            {
                RuleFor(a => a.Status)
                    .Must(x => Enum.IsDefined(typeof(PedidoStatusEnum), x))
                    .WithMessage(EnumMensagensErro.PedidoStatusInvalido.ToString());
            }

            return Validate(dto);
        }
    }
}
