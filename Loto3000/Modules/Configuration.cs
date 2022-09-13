using AutoMapper;
using HashidsNet;
using Loto3000.Application.Mapper;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class Configuration
    {
        public static IServiceCollection AddConfiguration(this IServiceCollection services)
        {
            services.AddSingleton(sp => ModelMapper.GetConfiguration());
            services.AddScoped(sp => sp.GetRequiredService<MapperConfiguration>().CreateMapper());

            services.AddScoped<IHashids>((sp) =>
            {
                var configuration = sp.GetRequiredService<IConfiguration>();
                var secret = configuration["Secret"];
                return new Hashids(secret);
            });

            return services;
        }
    }
}