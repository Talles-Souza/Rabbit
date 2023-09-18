using ConsumerRabbit.domain;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;
using System.Threading.Channels;

namespace ConsumerRabbit.Message
{
    public class Consumer : BackgroundService
    {
        private readonly IRepository _repository;
        private IConnection _connection;
        private IModel _channel;

        public Consumer(IRepository repository)
        {
            _repository = repository;
            var factory = new ConnectionFactory
            {
                HostName = "localhost",
                Port = 5672,
                UserName = "guest",
                Password = "guest"

            };
            
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: "checkoutMessage", false, false, false, arguments: null);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
           stoppingToken.ThrowIfCancellationRequested();
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (chanel, evt) =>
            {
                var content = Encoding.UTF8.GetString(evt.Body.ToArray());
                WeatherForecast teste = JsonSerializer.Deserialize<WeatherForecast>(content);
                ProcessConsumer(teste).GetAwaiter().GetResult();
                _channel.BasicAck(evt.DeliveryTag,false);
            };
            _channel.BasicConsume("checkoutMessage", false, consumer);
            return Task.CompletedTask;
        }

        private async Task ProcessConsumer(WeatherForecast teste)
        {
            await _repository.Create(teste);
        }
    }
}
