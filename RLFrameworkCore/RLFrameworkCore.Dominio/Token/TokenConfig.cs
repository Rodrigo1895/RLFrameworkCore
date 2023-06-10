namespace RLFrameworkCore.Dominio.Token
{
    public class TokenConfig
    {
        public Symmetric Symmetric { get; set; }
        public Asymmetric Asymmetric { get; set; }
        public string Issuer{ get; set; }
        public string Audience { get; set; }
        public int MinutosTokenValido { get; set; }
        public int MinutosRefreshTokenValido { get; set; }
    }

    public class Symmetric
    {
        public string Key { get; set; }
    }

    public class Asymmetric
    {
        public string PrivateKey { get; set; }
        public string PublicKey { get; set; }
    }
}
