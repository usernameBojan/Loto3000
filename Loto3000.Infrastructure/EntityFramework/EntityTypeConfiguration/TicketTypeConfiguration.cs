using Loto3000.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Loto3000.Infrastructure.EntityFramework.EntityTypeConfiguration
{
    internal class TicketTypeConfiguration
        : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            builder.Property(p => p.NumbersGuessed)
            .HasMaxLength(10)
            .IsRequired();

            builder.Property(p => p.TicketCreatedTime)
            .HasMaxLength(256)
            .IsRequired();

            builder.Property(p => p.CombinationNumbersString)
            .HasMaxLength(32)
            .IsRequired();
        }
    }
}