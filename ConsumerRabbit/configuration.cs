using ConsumerRabbit.data;
using ConsumerRabbit.domain;
using ConsumerRabbit.Message;
using Microsoft.EntityFrameworkCore;

namespace ConsumerRabbit
{
    public static class configuration
    {

        public static IServiceCollection AddInfrastruture(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ContextDb>(options => options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")), ServiceLifetime.Singleton);
            services.AddSingleton<IRepository, Repository>();
            return services;
        }
    }
}
