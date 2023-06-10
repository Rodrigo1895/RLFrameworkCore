using Exemplo.Web.RabbitMq.Consumer.Consumers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Exemplo.Web.RabbitMq.Consumer
{
    public class Program
    {
        static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            var configuration = builder.Build();

            return Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddRabbitMqConfig(configuration);
                    services.AddHostedService<RabbitMqConsumerPedidoConcluidoFinanceiro>();
                    services.AddHostedService<RabbitMqConsumerPedidoConcluidoEmail>();
                });
        }
    }
}