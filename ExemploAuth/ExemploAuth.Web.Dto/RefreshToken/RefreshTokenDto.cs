using RLFrameworkCore.Dto.DTOs;

namespace ExemploAuth.Web.Dto.RefreshToken
{
    public class RefreshTokenDto : DtoBase
    {
        public string Token { get; set; }
        public DateTime DataExpiracao { get; set; }
    }
}