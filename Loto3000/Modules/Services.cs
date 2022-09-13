using Loto3000.Application.Services;
using Loto3000.Application.Services.Implementation;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class Services
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IAdminService, AdminService>();
            services.AddScoped<IPlayerService, PlayerService>();
            services.AddScoped<ILoginService, LoginService>();  
            services.AddScoped<IDrawService, DrawService>();

            return services;
        }
    }
}