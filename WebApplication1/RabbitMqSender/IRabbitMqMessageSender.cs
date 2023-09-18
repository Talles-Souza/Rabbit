using Service;

namespace WebApplication1.RabbitMqSender
{
    public interface IRabbitMqMessageSender
    {
        void Send(BaseMessage message,string queueName);
        void Direct(BaseMessage message,string queueName);
    }
}
