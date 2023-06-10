using ExemploAuth.Web.Dominio.Entidades;
using ExemploAuth.Web.Dto.Usuario;
using System.Security.Claims;

namespace ExemploAuth.Web.Aplicacao.Servicos.Autenticacao
{
    public interface IGerarToken
    {
        Task<UsuarioAutenticadoDto> GerarToken(UsuarioEntidade usuario, IList<Claim> claims);
        ClaimsPrincipal GetClaimsPrincipalFromExpiredToken(string token);
    }
}