using Exemplo.Web.Dominio;
using Exemplo.Web.Dominio.Entidades.Cliente;
using Exemplo.Web.Dominio.Interfaces.Repositorios;
using Exemplo.Web.Dominio.Localizacao;
using Exemplo.Web.Dto.Cliente.Request;
using Microsoft.EntityFrameworkCore;
using RLFrameworkCore.Dto.DTOs.Validacao;
using RLFrameworkCore.Notificacao.Notificacao.Interfaces;

namespace Exemplo.Web.Aplicacao.Servicos.Cliente.Validacoes
{
    public class ValidarAdicionarClienteDto : IValidacaoDtoAsync<AdicionarClienteDto>
    {
        private readonly IClienteRepositorio _clienteRepositorio;

        public ValidarAdicionarClienteDto(IClienteRepositorio clienteRepositorio)
        {
            _clienteRepositorio = clienteRepositorio;
        }

        public async Task ValidarDtoAsync(AdicionarClienteDto dto, INotificacaoContexto notificacao)
        {
            var count = await _clienteRepositorio.BuscarTodos(x => x.CPF == dto.CPF).CountAsync();
            if(count > 0)
            {
                notificacao.AddNotificacao(nameof(ClienteEntidade.CPF), EnumMensagensErro.CPFJaCadastrado, Constants.NomeErroSource);
            }
        }
    }
}
