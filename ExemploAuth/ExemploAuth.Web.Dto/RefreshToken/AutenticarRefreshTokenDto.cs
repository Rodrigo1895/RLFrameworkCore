using RLFrameworkCore.Dto.DTOs.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace ExemploAuth.Web.Dto.RefreshToken
{
    public class AutenticarRefreshTokenDto : IDto
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}