using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RLFrameworkCore.Dominio.Token;
using RLFrameworkCore.Dominio.Token.TokenInfo;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class AutenticacaoExtension
    {
        public static IServiceCollection AddAutenticacao(this IServiceCollection services, IConfiguration config, bool authAsymmetric = true)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<ITokenInfo, TokenInfo>();

            services.Configure<TokenConfig>(config.GetSection("TokenConfig"));
            var tokenConfig = services.BuildServiceProvider().GetRequiredService<IOptions<TokenConfig>>().Value;

            if (authAsymmetric)
            {
                if (tokenConfig?.Asymmetric == null || string.IsNullOrWhiteSpace(tokenConfig?.Asymmetric?.PublicKey))
                {
                    throw new ArgumentException("Chave Assimétrica Pública não informada no arquivo de configurações");
                }
                AddAutenticacaoAsymmetric(services, tokenConfig);
            }
            else
            {
                if (tokenConfig?.Symmetric == null || string.IsNullOrWhiteSpace(tokenConfig?.Symmetric?.Key))
                {
                    throw new ArgumentException("Chave Simétrica não informada no arquivo de configurações");
                }
                AddAutenticacaoSymmetric(services, tokenConfig);
            }


            return services;
        }

        private static void AddAutenticacaoSymmetric(IServiceCollection services, TokenConfig tokenConfig)
        {            
            var key = Encoding.ASCII.GetBytes(tokenConfig.Symmetric.Key);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = tokenConfig.Issuer,
                    ValidAudience = tokenConfig.Audience,
                    RequireSignedTokens = true,
                    RequireExpirationTime = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });
        }

        private static void AddAutenticacaoAsymmetric(IServiceCollection services, TokenConfig tokenConfig)
        {
            var issuerSigningCertificate = new SigningIssuerCertificate();
            RsaSecurityKey issuerSigningKey = issuerSigningCertificate.GetIssuerSigningKey(tokenConfig);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = issuerSigningKey,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = tokenConfig.Issuer,
                    ValidAudience = tokenConfig.Audience,
                    RequireSignedTokens = true,
                    RequireExpirationTime = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });
        }
    }
}