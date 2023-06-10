using RLFrameworkCore.Dto.DTOs.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace ExemploAuth.Web.Dto.Usuario
{
    public class AutenticarUsuarioLoginDto : IDto
    {
        public string Nome { get; set; }
        public string Senha { get; set; }
    }
}
