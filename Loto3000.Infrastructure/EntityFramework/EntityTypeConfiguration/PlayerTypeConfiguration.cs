using Loto3000.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Loto3000.Infrastructure.EntityFramework.EntityTypeConfiguration
{
    public class PlayerTypeConfiguration
        : IEntityTypeConfiguration<Player>
    {
        public void Configure(EntityTypeBuilder<Player> builder)
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

            builder.Property(p => p.Credits)
            .HasMaxLength(50)
            .IsRequired();

            builder.Property(p => p.Email)
            .HasMaxLength(512)
            .IsRequired();

            builder.Property(p => p.Role)
            .HasMaxLength(50)
            .IsRequired();

            builder.Property(p => p.DateOfBirth)
            .HasMaxLength(256)
            .IsRequired();

            builder.ToTable("Players");
        }
    }
}