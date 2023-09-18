using ConsumerRabbit.data;
using System;

namespace ConsumerRabbit.domain
{
    public class Repository : IRepository
    {
        private readonly ContextDb _db;

        public Repository(ContextDb db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }
        public async Task<WeatherForecast> Create(WeatherForecast teste)
        {
            WeatherForecast teste1 = new WeatherForecast();
            teste1.Summary = teste.Summary;
            teste1.TemperatureC = teste.TemperatureC;
            teste1.Date = DateTime.UtcNow.Date;

            _db.Teste.Add(teste1);
            await _db.SaveChangesAsync();
            return teste;
        }
    }
}
