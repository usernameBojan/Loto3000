using Loto3000.Application.Services;
using Loto3000.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Loto3000.Infrastructure.EntityFramework.EntityTypeConfiguration
{
    public class SuperAdminTypeConfiguration : IEntityTypeConfiguration<SuperAdmin>
    {
        PasswordHasher hasher = new PasswordHasher();
        public void Configure(EntityTypeBuilder<SuperAdmin> builder)
        {
            builder.HasData(
                new SuperAdmin 
                {
                    Id = 3,
                    FirstName = "Super",
                    LastName = "Admin",
                    Username = "SystemAdministrator",
                    Password = "123456789101112",
                    Role = "SuperAdmin"
                }
                //FOR TESTING ONLY, SEEDING LIKE THIS - very BAD IDEA
            );
        }
    }
}