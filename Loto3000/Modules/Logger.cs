using Serilog;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class Logger
    {
        public static IServiceCollection AddLogger(this IServiceCollection services, IConfiguration configuration)
        {
            Log.Logger = new LoggerConfiguration()
                                         .ReadFrom
                                         .Configuration(configuration)
                                         .CreateLogger();

            services.AddSingleton<Serilog.ILogger>(Log.Logger);

            return services;
        }
    }
}