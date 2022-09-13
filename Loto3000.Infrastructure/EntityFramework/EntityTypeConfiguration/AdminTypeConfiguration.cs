using Loto3000.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Loto3000.Infrastructure.EntityFramework.EntityTypeConfiguration
{
    public class AdminTypeConfiguration : IEntityTypeConfiguration<Admin>
    {
        public void Configure(EntityTypeBuilder<Admin> builder)
        {

            builder.Property(p => p.FirstName)
            .HasMaxLength(128)
            .IsRequired();

            builder.Property(p => p.LastName)
            .HasMaxLength(128)
            .IsRequired();

            builder.Property(p => p.Username)
            .HasMaxLength(256)
            .IsRequired();

            builder.Property(p => p.Password)
             .HasMaxLength(128)
             .IsRequired();

            builder.Property(p => p.Role)
            .HasMaxLength(50)
            .IsRequired();
        }
    }
}