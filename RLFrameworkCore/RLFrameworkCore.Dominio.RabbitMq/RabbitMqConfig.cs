namespace RLFrameworkCore.Dominio.RabbitMq
{
    public sealed class RabbitMqConfig
    {
        public string HostName { get; set; }
        public int Port { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string AppId { get; set; }
    }
}