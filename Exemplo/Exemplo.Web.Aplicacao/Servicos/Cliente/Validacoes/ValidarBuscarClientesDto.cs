using Exemplo.Web.Dominio;
using Exemplo.Web.Dominio.Entidades.Cliente.Enums;
using Exemplo.Web.Dominio.Localizacao;
using Exemplo.Web.Dto.Cliente.Request;
using FluentValidation;
using FluentValidation.Results;
using RLFrameworkCore.Dto.DTOs.Validacao;

namespace Exemplo.Web.Aplicacao.Servicos.Cliente.Validacoes
{
    public class ValidarBuscarClientesDto : AbstractValidator<BuscarClientesDto>, IValidacaoDtoFluent<BuscarClientesDto>
    {
        public string NomeArquivo => Constants.NomeErroSource;

        public ValidationResult ValidarDtoComFluent(BuscarClientesDto dto)
        {
            if (!string.IsNullOrEmpty(dto.Genero))
            {
                RuleFor(a => a.Genero)
                    .Must(x => Enum.IsDefined(typeof(GeneroEnum), x))
                    .WithMessage(EnumMensagensErro.GeneroInvalido.ToString());
            }

            return Validate(dto);
        }
    }
}
