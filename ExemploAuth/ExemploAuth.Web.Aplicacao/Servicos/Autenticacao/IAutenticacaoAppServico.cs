using ExemploAuth.Web.Dto.RefreshToken;
using ExemploAuth.Web.Dto.Usuario;
using RLFrameworkCore.Dominio.Servico.Interfaces;

namespace ExemploAuth.Web.Aplicacao.Servicos.Autenticacao
{
    public interface IAutenticacaoAppServico : IAppServicoBase
    {
        Task<UsuarioAutenticadoDto> AutenticarUsuarioLoginAsync(AutenticarUsuarioLoginDto dto);
        Task<UsuarioAutenticadoDto> AutenticarRefreshTokenAsync(AutenticarRefreshTokenDto dto);
    }
}
