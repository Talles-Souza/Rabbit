using ConsumerRabbit.domain;
using Microsoft.AspNetCore.Mvc;

namespace ConsumerRabbit.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IRepository _repository;

        public WeatherForecastController(IRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] WeatherForecast dto)
        {
            var response = await _repository.Create(dto);        
            return Ok(response);
        }
    }
}