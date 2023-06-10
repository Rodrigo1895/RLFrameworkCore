using ExemploAuth.Web.Dto.RefreshToken;
using RLFrameworkCore.Dto.DTOs;

namespace ExemploAuth.Web.Dto.Usuario
{
    public class UsuarioAutenticadoDto : DtoBase
    {
        public int IdUsuario { get; set; }
        public string AccessToken { get; set; }
        public DateTime DataExpiracao { get; set; }
        public RefreshTokenDto RefreshToken { get; set; }
    }
}
