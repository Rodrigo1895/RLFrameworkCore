using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace RLFrameworkCore.Dominio.Token.TokenInfo
{
    internal class TokenInfo : ITokenInfo
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public TokenInfo(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int? ObterIdUsuario()
        {
            try
            {
                if (_httpContextAccessor.HttpContext?.User?.Identity is ClaimsIdentity identity)
                {
                    var id = identity.FindFirst("idUsuario")?.Value;
                    return Int32.Parse(id);
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
