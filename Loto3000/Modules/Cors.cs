namespace Microsoft.Extensions.DependencyInjection
{
    public static class Cors
    {
        private const string CorsPolicy = "CorsPolicy"; 
        public static IServiceCollection AddCorsPolicy(this IServiceCollection services)
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