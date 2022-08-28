using Microsoft.EntityFrameworkCore;
using Loto3000.Domain.Entities;

namespace Loto3000.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<SuperAdmin> SuperAdmin {get; set;}
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Draw> Draws { get; set; }
        public DbSet<TransactionTracker> Transactions { get; set; }
    }
}