using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;

namespace RLFrameworkCore.Dominio.Token
{
    public class SigningIssuerCertificate : IDisposable
    {
        private readonly RSA rsa;

        public SigningIssuerCertificate()
        {
            rsa = RSA.Create();
        }

        public RsaSecurityKey GetIssuerSigningKey(TokenConfig tokenConfig)
        {
            rsa.ImportRSAPublicKey(
                    source: Convert.FromBase64String(tokenConfig.Asymmetric.PublicKey),
                    bytesRead: out int _
                );

            return new RsaSecurityKey(rsa);
        }

        public void Dispose()
        {
            rsa?.Dispose();
        }
    }
}
