using Loto3000.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Loto3000.Infrastructure.EntityFramework.EntityTypeConfiguration
{
    public class SuperAdminTypeConfiguration : IEntityTypeConfiguration<SuperAdmin>
    {
        public void Configure(EntityTypeBuilder<SuperAdmin> builder)
        {
            builder.HasData(
                new SuperAdmin(1, "SystemAdministrator", "123456789101112", "SuperAdmin")
            //FOR TESTING ONLY, SEEDING ADMINS LIKE THIS - very BAD IDEA
            );
        }
    }
}