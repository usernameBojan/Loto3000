using Loto3000.Middlewares;

namespace Microsoft.Extensions.DependencyInjection
{
    internal static class GlobalExceptionHandlerExtension
    {
        internal static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder app)
        {
            app.UseMiddleware<GlobalExceptionHandler>();
            return app;
        }
    }
}