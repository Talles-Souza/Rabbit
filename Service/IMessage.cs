namespace Service
{
    public interface IMessage
    {
        Task PublicMessage(BaseMessage message, string queueName);
    }
}