using ExemploAuth.Web.Dominio;
using ExemploAuth.Web.Dominio.Entidades;
using ExemploAuth.Web.Dominio.Interfaces.Repositorios;
using ExemploAuth.Web.Dominio.Localizacao;
using ExemploAuth.Web.Dto.RefreshToken;
using ExemploAuth.Web.Dto.Usuario;
using RLFrameworkCore.Dominio.Servico;
using RLFrameworkCore.Notificacao.Notificacao.Interfaces;
using System.Security.Claims;

namespace ExemploAuth.Web.Aplicacao.Servicos.Autenticacao
{
    public class AutenticacaoAppServico : AppServicoBase, IAutenticacaoAppServico
    {
        private readonly IRefreshTokenRepositorio _refreshTokenRepositorio;
        private readonly IGerarToken _gerarToken;
        private readonly IUsuarioRepositorio _usuarioRepositorio;

        public AutenticacaoAppServico(INotificacaoContexto notificacao,
            IRefreshTokenRepositorio refreshTokenRepositorio,
            IGerarToken gerarToken,
            IUsuarioRepositorio usuarioRepositorio) : base(notificacao)
        {
            _refreshTokenRepositorio = refreshTokenRepositorio;
            _gerarToken = gerarToken;
            _usuarioRepositorio = usuarioRepositorio;
        }

        public async Task<UsuarioAutenticadoDto> AutenticarUsuarioLoginAsync(AutenticarUsuarioLoginDto dto)
        {
            var usuario = await _usuarioRepositorio.BuscarAsync(x => x.Nome == dto.Nome);
            if (usuario == null)
            {
                Notificacao.AddNotificacao("nome", EnumMensagensErro.UsuarioNaoEncontrado, Constants.NomeErroSource);
                return null;
            }

            usuario = await _usuarioRepositorio.BuscarAsync(x => x.Nome == dto.Nome && x.Senha == dto.Senha);
            if (usuario == null)
            {
                Notificacao.AddNotificacao("senha", EnumMensagensErro.SenhaInvalida, Constants.NomeErroSource);
                return null;
            }

            return await _gerarToken.GerarToken(usuario, GetClaims(usuario));
        }

        public async Task<UsuarioAutenticadoDto> AutenticarRefreshTokenAsync(AutenticarRefreshTokenDto dto)
        {
            var claimsPrincipal = _gerarToken.GetClaimsPrincipalFromExpiredToken(dto.AccessToken);
            if (Notificacao.HasNotificacoes)
                return null;

            var idUsuario = claimsPrincipal.Claims.Where(x => x.Type == "idUsuario").FirstOrDefault()?.Value;
            var nome = claimsPrincipal.Claims.Where(x => x.Type == ClaimTypes.Name).FirstOrDefault()?.Value;
            if (string.IsNullOrWhiteSpace(idUsuario) || string.IsNullOrWhiteSpace(nome))
            {
                Notificacao.AddNotificacao("accessToken", EnumMensagensErro.AccessTokenInvalido, Constants.NomeErroSource);
                return null;
            }

            var usuario = await _usuarioRepositorio.BuscarAsync(x => x.Nome == nome);
            if (usuario == null)
            {
                Notificacao.AddNotificacao("usuario", EnumMensagensErro.UsuarioNaoEncontrado, Constants.NomeErroSource);
                return null;
            }

            var refreshToken = await _refreshTokenRepositorio.BuscarAsync(x => x.UsuarioId == Convert.ToInt32(idUsuario) &&
                    x.Token == dto.RefreshToken);

            if (refreshToken == null || refreshToken.DataExpiracao < DateTime.Now)
            {
                Notificacao.AddNotificacao("refreshToken", EnumMensagensErro.RefreshTokenInvalido, Constants.NomeErroSource);
                return null;
            }

            return await _gerarToken.GerarToken(usuario, GetClaims(usuario));
        }

        private IList<Claim> GetClaims(UsuarioEntidade usuario)
        {
            IList<Claim> claims = new List<Claim>
            {
                new Claim("idUsuario", usuario.Id.ToString()),
                new Claim(ClaimTypes.Name, usuario.Nome.ToString())
            };

            return claims;
        }
    }
}
