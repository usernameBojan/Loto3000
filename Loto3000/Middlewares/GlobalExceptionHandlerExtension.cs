﻿using Loto3000.Middlewares;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class GlobalExceptionHandlerExtension
    {
        public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder app)
        {
            app.UseMiddleware<GlobalExceptionHandler>();
            return app;
        }
    }
}