using Loto3000.Application.Services;
using Loto3000.Application.Services.Implementation;

namespace Microsoft.Extensions.DependencyInjection
{
    internal static class Services
    {
        internal static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IAdminService, AdminService>();
            services.AddScoped<IPlayerService, PlayerService>();
            services.AddScoped<ILoginService, LoginService>();  
            services.AddScoped<IDrawService, DrawService>();
            services.AddScoped<ITicketService, TicketService>();
            services.AddScoped<ITransactionService, TransactionService>();

            return services;
        }
    }
}