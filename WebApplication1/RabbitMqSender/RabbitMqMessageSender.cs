using RabbitMQ.Client;
using Service;
using System.Text;
using System.Text.Json;

namespace WebApplication1.RabbitMqSender
{
    public class RabbitMqMessageSender : IRabbitMqMessageSender
    {
        private readonly string _hostName;
        private readonly string _password;
        private readonly string _userName;
        private IConnection _connection;

        public RabbitMqMessageSender()
        {
            _hostName = "localhost";
            _password = "guest";
            _userName = "guest";
        }

        public void Direct(BaseMessage message, string queueName)
        {
            var factory = new ConnectionFactory
            {
                HostName = _hostName,
                Port = 5672,
                UserName = _userName,
                Password = _password
            };
            _connection = factory.CreateConnection();

            using var channel = _connection.CreateModel();

            // Declare a exchange do tipo 'direct'
            channel.ExchangeDeclare(exchange: "test_direct", type: "direct");

            // Publica a mensagem na exchange usando a chave de roteamento
            byte[] body = GetMessageAsByreArray(message);
            channel.BasicPublish(exchange: "test_direct", routingKey: queueName, basicProperties: null, body: body);

        }

        public void Send(BaseMessage message, string queueName)
        {
            var factory = new ConnectionFactory
            {
                HostName = _hostName,
                Port = 5672,
                UserName = _userName,
                Password = _password

            };
            _connection = factory.CreateConnection();

            using var channel = _connection.CreateModel();

            channel.QueueDeclare(queue: queueName, false,false,false, arguments: null);
            byte[] body = GetMessageAsByreArray(message);
            channel.BasicPublish(exchange:"",routingKey: queueName, basicProperties:null,body:body);

        }


        private byte[] GetMessageAsByreArray(BaseMessage message)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
            };
           var json = JsonSerializer.Serialize<WeatherForecast>((WeatherForecast)message, options);
           var body = Encoding.UTF8.GetBytes(json);
           return body;
        }
    }
}
