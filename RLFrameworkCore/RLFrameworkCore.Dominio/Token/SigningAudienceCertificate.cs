using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;

namespace RLFrameworkCore.Dominio.Token
{
    public class SigningAudienceCertificate : IDisposable
    {
        private readonly RSA rsa;

        public SigningAudienceCertificate()
        {
            rsa = RSA.Create();
        }

        public SigningCredentials GetAudienceSigningKey(TokenConfig tokenConfig)
        {
            rsa.ImportRSAPrivateKey( 
                source: Convert.FromBase64String(tokenConfig.Asymmetric.PrivateKey),
                bytesRead: out int _);

            return new SigningCredentials(
                key: new RsaSecurityKey(rsa),
                algorithm: SecurityAlgorithms.RsaSha256
            );
        }

        public void Dispose()
        {
            rsa?.Dispose();
        }
    }
}
