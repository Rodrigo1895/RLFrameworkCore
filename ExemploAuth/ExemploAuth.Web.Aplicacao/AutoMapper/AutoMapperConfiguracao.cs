using ExemploAuth.Web.Aplicacao.AutoMapper.Profiles.RefreshToken;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class AutoMapperConfiguracao
    {
        public static void AddAutoMapperConfiguracao(this IServiceCollection services)
        {
            services.AddAutoMapper(
                
                typeof(RefreshTokenProfile)
            );
        }
    }
}