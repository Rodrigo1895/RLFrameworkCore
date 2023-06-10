using ExemploAuth.Web.Dominio.Entidades;
using ExemploAuth.Web.Dominio.Interfaces.Repositorios;
using ExemploAuth.Web.Dto.Usuario;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RLFrameworkCore.Dominio.Token;
using RLFrameworkCore.Notificacao.Notificacao.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ExemploAuth.Web.Aplicacao.Servicos.Autenticacao
{
    public class GerarTokenSymmetric : GerarTokenBase
    {
        private readonly TokenConfig _tokenConfig;

        public GerarTokenSymmetric(INotificacaoContexto notificacaoContexto,
            IOptions<TokenConfig> tokenConfig,
            IRefreshTokenRepositorio refreshTokenRepositorio) : base(notificacaoContexto, tokenConfig, refreshTokenRepositorio)
        {
            _tokenConfig = tokenConfig.Value;
        }

        public override async Task<UsuarioAutenticadoDto> GerarToken(UsuarioEntidade usuario, IList<Claim> claims)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_tokenConfig.Symmetric.Key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _tokenConfig.Issuer,
                Audience = _tokenConfig.Audience,
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(_tokenConfig.MinutosTokenValido),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(securityToken);

            var dto = new UsuarioAutenticadoDto();
            dto.IdUsuario = usuario.Id;
            dto.AccessToken = token;
            dto.RefreshToken = await GerarRefreshToken(usuario);

            return dto;
        }
    }
}