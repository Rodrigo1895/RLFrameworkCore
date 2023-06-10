using Exemplo.Web.Dominio;
using Exemplo.Web.Dominio.Localizacao;
using Exemplo.Web.Dto.Pedido.Request;
using RLFrameworkCore.Dto.DTOs.Validacao;
using RLFrameworkCore.Notificacao.Notificacao.Interfaces;

namespace Exemplo.Web.Aplicacao.Servicos.Pedido.Validacoes
{
    public class ValidarAdicionarPedidoDto : IValidacaoDto<AdicionarPedidoDto>
    {
        public void ValidarDto(AdicionarPedidoDto dto, INotificacaoContexto notificacao)
        {
            if(dto.PedidoItens == null || dto.PedidoItens.Count == 0)
            {
                notificacao.AddNotificacao("", EnumMensagensErro.PedidoSemItens, Constants.NomeErroSource);
            }
        }
    }
}