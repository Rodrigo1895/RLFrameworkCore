using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace System
{
    public static class AutoMapperHelper
    {
        private static IServiceProvider ServiceProvider;
        public static void UseAutoMapperHelper(this IApplicationBuilder applicationBuilder)
        {
            ServiceProvider = applicationBuilder.ApplicationServices;
        }

        public static TDest MapTo<TDest>(this object source)
        {
            var mapper = ServiceProvider.GetRequiredService<IMapper>();
            return mapper.Map<TDest>(source);
        }

        public static TDest MapTo<TSource, TDest>(this TSource source)
        {
            var mapper = ServiceProvider.GetRequiredService<IMapper>();
            return mapper.Map<TSource, TDest>(source);
        }

        public static TDest MapTo<TSource, TDest>(this TSource source, TDest dest)
        {
            var mapper = ServiceProvider.GetRequiredService<IMapper>();
            return mapper.Map(source, dest);
        }        
    }
}
