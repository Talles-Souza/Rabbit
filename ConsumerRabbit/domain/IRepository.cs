using System;

namespace ConsumerRabbit.domain
{
    public interface IRepository
    {

        Task<WeatherForecast> Create(WeatherForecast teste);
    }
}
