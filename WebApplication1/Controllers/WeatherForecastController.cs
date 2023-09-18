using Microsoft.AspNetCore.Mvc;
using System;
using System.Reflection;
using WebApplication1.RabbitMqSender;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;
        private IRabbitMqMessageSender _rabbit;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IRabbitMqMessageSender rabbit)
        {
            _logger = logger;
            _rabbit = rabbit;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            var body = new WeatherForecast
            {
                Date = DateTime.Now.AddDays(20),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            };

            /* _rabbit.Send(body,"checkoutMessage");*/

            for (int i = 0; i < 100; i++)
            {
               /* _rabbit.Send(body, "checkoutMessage");*/
                _rabbit.Direct(body, "checkoutMessage");
            }

            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}