using Loto3000.Middlewares;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class LoggerExtension
    {
        public static IApplicationBuilder UseCustomLogger(this IApplicationBuilder app)
        {
            app.UseMiddleware<Loto3000.Middlewares.Logger>();
            return app;
        }
    }
}