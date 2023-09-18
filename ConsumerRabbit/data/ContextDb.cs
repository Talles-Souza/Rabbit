using ConsumerRabbit.domain;
using Microsoft.EntityFrameworkCore;
using System;

namespace ConsumerRabbit.data
{
    public class ContextDb : DbContext
    {
        public ContextDb() { }
        public ContextDb(DbContextOptions<ContextDb> options) : base(options)
        { }

        public DbSet<WeatherForecast> Teste { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //builder.ApplyConfigurationsFromAssembly(typeof(ContextDb).Assembly);
        }
    }
}
