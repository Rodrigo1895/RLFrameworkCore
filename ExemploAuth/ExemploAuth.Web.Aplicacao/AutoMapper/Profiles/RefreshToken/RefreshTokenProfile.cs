using AutoMapper;
using ExemploAuth.Web.Dominio.Entidades;
using ExemploAuth.Web.Dto.RefreshToken;

namespace ExemploAuth.Web.Aplicacao.AutoMapper.Profiles.RefreshToken
{
    public class RefreshTokenProfile : Profile
    {
        public RefreshTokenProfile()
        {
            CreateMap<RefreshTokenEntidade, RefreshTokenDto>();
        }
    }
}
