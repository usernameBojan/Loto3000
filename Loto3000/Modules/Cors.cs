namespace Microsoft.Extensions.DependencyInjection
{
    internal static class Cors
    {
        internal const string CorsPolicy = "CorsPolicy";
        internal static IServiceCollection AddCorsPolicy(this IServiceCollection services)
        {
            services.AddCors(opts =>
            {
                opts.AddPolicy(CorsPolicy, policyBuilder => policyBuilder.WithOrigins("http://localhost:3000")
                                                                         .AllowAnyHeader()
                                                                         .AllowAnyMethod()
                                                                         .AllowCredentials());
            });

            return services;
        }
    }
}