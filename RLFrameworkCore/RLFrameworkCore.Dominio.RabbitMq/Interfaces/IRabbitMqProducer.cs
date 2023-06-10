namespace RLFrameworkCore.Dominio.RabbitMq.Interfaces
{
    public interface IRabbitMqProducer<T>
    {
        void Publish(T message);
    }
}
