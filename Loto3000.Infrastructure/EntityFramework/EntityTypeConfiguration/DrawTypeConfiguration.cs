using Loto3000.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Loto3000.Infrastructure.EntityFramework.EntityTypeConfiguration
{
    public class DrawTypeConfiguration : IEntityTypeConfiguration<Draw>
    {

        public void Configure(EntityTypeBuilder<Draw> builder)
        {
            builder.Property(p => p.DrawNumbersString)
            .HasMaxLength(64)
            .IsRequired();

            builder.Property(p => p.DrawTime)
            .HasMaxLength(64)
            .IsRequired();

            builder.Property(p => p.SessionStart)
            .HasMaxLength(64)
            .IsRequired();

            builder.Property(p => p.SessionEnd)
            .HasMaxLength(64)
            .IsRequired();

            builder.HasData(Draw.SetFirstSession());
        }
    }
}