using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RLFrameworkCore.Dominio.RabbitMq;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class RabbitMqExtensions
    {
        public static IServiceCollection AddRabbitMqConfig(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<RabbitMqConfig>(config.GetSection("RabbitMqConfig"));
            var rabbitMqConfig = services.BuildServiceProvider().GetRequiredService<IOptions<RabbitMqConfig>>().Value;

            var connectionFactory = new ConnectionFactory
            {
                HostName = rabbitMqConfig.HostName,
                Port = rabbitMqConfig.Port,
                UserName = rabbitMqConfig.UserName,
                Password = rabbitMqConfig.Password
            };
            services.AddSingleton(connectionFactory);

            var connection = connectionFactory.CreateConnection();
            services.AddSingleton(connection);
            services.AddSingleton(new RabbitMqConnection(connectionFactory, connection));

            return services;
        }
    }
}
