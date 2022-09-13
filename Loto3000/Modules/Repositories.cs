using Loto3000.Application.Repositories;
using Loto3000.Application.Services;
using Loto3000.Domain.Entities;
using Loto3000.Infrastructure;
using Loto3000.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class Repositories
    {
        public static IServiceCollection AddInfrastracture(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IPasswordHasher, PasswordHasher>();
            services.AddScoped<IRepository<User>, BaseRepository<User>>();
            services.AddScoped<IRepository<Player>, BaseRepository<Player>>();
            services.AddScoped<IRepository<Admin>, BaseRepository<Admin>>();
            services.AddScoped<IRepository<SuperAdmin>, BaseRepository<SuperAdmin>>();
            services.AddScoped<IRepository<Draw>, BaseRepository<Draw>>();
            services.AddScoped<IRepository<Ticket>, BaseRepository<Ticket>>();
            services.AddScoped<IRepository<Combination>, BaseRepository<Combination>>();
            services.AddScoped<IRepository<DrawNumbers>, BaseRepository<DrawNumbers>>();
            services.AddScoped<IRepository<TransactionTracker>, BaseRepository<TransactionTracker>>();

            services.AddDbContext<ApplicationDbContext>(opts => opts.UseSqlServer(configuration.GetConnectionString("DefaultConnectionString")));
            
            return services;
        }
    }
}