using Microsoft.EntityFrameworkCore;
using Loto3000.Domain.Entities;
using Loto3000.Infrastructure.EntityFramework.EntityTypeConfiguration;

namespace Loto3000.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new PlayerTypeConfiguration());
            builder.ApplyConfiguration(new AdminTypeConfiguration());
            builder.ApplyConfiguration(new SuperAdminTypeConfiguration());
            builder.ApplyConfiguration(new DrawTypeConfiguration());
            builder.ApplyConfiguration(new TicketTypeConfiguration());
        }
        public DbSet<SuperAdmin> SuperAdmin {get; set;}
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Draw> Draws { get; set; }
        public DbSet<TransactionTracker> Transactions { get; set; }
    }
}