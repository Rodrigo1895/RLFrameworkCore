using ExemploAuth.Web.Dominio.Entidades;
using ExemploAuth.Web.Dominio.Interfaces.Repositorios;
using ExemploAuth.Web.Dto.Usuario;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RLFrameworkCore.Dominio.Token;
using RLFrameworkCore.Notificacao.Notificacao.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ExemploAuth.Web.Aplicacao.Servicos.Autenticacao
{
    public class GerarTokenAsymmetric : GerarTokenBase
    {
        private readonly SigningAudienceCertificate _signingAudienceCertificate;
        private readonly TokenConfig _tokenConfig;

        public GerarTokenAsymmetric(INotificacaoContexto notificacaoContexto,
            IOptions<TokenConfig> tokenConfig,
            IRefreshTokenRepositorio refreshTokenRepositorio) : base(notificacaoContexto, tokenConfig, refreshTokenRepositorio)
        {
            _signingAudienceCertificate = new SigningAudienceCertificate();
            _tokenConfig = tokenConfig.Value;
        }

        public override async Task<UsuarioAutenticadoDto> GerarToken(UsuarioEntidade usuario, IList<Claim> claims)
        {
            var dtaExpiracao = DateTime.Now.AddMinutes(_tokenConfig.MinutosTokenValido);

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _tokenConfig.Issuer,
                Audience = _tokenConfig.Audience,
                Subject = new ClaimsIdentity(claims),
                Expires = dtaExpiracao,
                SigningCredentials = _signingAudienceCertificate.GetAudienceSigningKey(_tokenConfig),
                NotBefore = DateTime.Now
            };
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(securityToken);

            var dto = new UsuarioAutenticadoDto
            {
                IdUsuario = usuario.Id,
                AccessToken = token,
                DataExpiracao = dtaExpiracao,
                RefreshToken = await GerarRefreshToken(usuario)
            };

            return dto;
        }
    }
}
