using ExemploAuth.Web.Dominio.Entidades;
using ExemploAuth.Web.Dominio.Interfaces.Repositorios;
using ExemploAuth.Web.Dto.RefreshToken;
using ExemploAuth.Web.Dto.Usuario;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RLFrameworkCore.Dominio.Token;
using RLFrameworkCore.Notificacao.Notificacao.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace ExemploAuth.Web.Aplicacao.Servicos.Autenticacao
{
    public abstract class GerarTokenBase : IGerarToken
    {
        private readonly INotificacaoContexto _notificacaoContexto;
        private readonly TokenConfig _tokenConfig;
        private readonly IRefreshTokenRepositorio _refreshTokenRepositorio;

        public GerarTokenBase(INotificacaoContexto notificacaoContexto,
            IOptions<TokenConfig> tokenConfig,
            IRefreshTokenRepositorio refreshTokenRepositorio)
        {
            _notificacaoContexto = notificacaoContexto;
            _tokenConfig = tokenConfig.Value;
            _refreshTokenRepositorio = refreshTokenRepositorio;
        }

        public abstract Task<UsuarioAutenticadoDto> GerarToken(UsuarioEntidade usuario, IList<Claim> claims);

        protected async Task<RefreshTokenDto> GerarRefreshToken(UsuarioEntidade usuario)
        {
            var dtaExpiracao = DateTime.Now.AddMinutes(_tokenConfig.MinutosRefreshTokenValido);

            string token;
            var randomNumber = new byte[32];

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                token = Convert.ToBase64String(randomNumber);
            }

            token = token.Replace("+", string.Empty)
                         .Replace("=", string.Empty)
                         .Replace("/", string.Empty);
            var refreshToken = new RefreshTokenEntidade(usuario.Id,
                                        token: token,
                                        dataExpiracao: dtaExpiracao);

            await _refreshTokenRepositorio.DeletarAsync(x => x.UsuarioId == usuario.Id);
            await _refreshTokenRepositorio.AdicionarAsync(refreshToken);
            return refreshToken.MapTo<RefreshTokenDto>();
        }

        public ClaimsPrincipal GetClaimsPrincipalFromExpiredToken(string token)
        {
            var issuerSigningCertificate = new SigningIssuerCertificate();
            RsaSecurityKey issuerSigningKey = issuerSigningCertificate.GetIssuerSigningKey(_tokenConfig);

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = issuerSigningKey,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidIssuer = _tokenConfig.Issuer,
                ValidAudience = _tokenConfig.Audience,
                RequireSignedTokens = true,
                RequireExpirationTime = true,
                ValidateLifetime = false
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var claimsPrincipal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.RsaSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                _notificacaoContexto.AddNotificacao("accessToken", "Access Token inválido.");
                return null;
            }

            return claimsPrincipal;
        }
    }
}