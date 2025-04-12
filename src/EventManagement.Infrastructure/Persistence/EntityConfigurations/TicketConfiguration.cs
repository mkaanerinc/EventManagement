using EventManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Infrastructure.Persistence.EntityConfigurations;

/// <summary>
/// Configures the database schema for the Ticket entity.
/// </summary>
public class TicketConfiguration : IEntityTypeConfiguration<Ticket>
{
    // <summary>
    /// Configures the Ticket entity's database mapping.
    /// </summary>
    /// <param name="builder">An instance of EntityTypeBuilder used to configure the entity.</param>
    public void Configure(EntityTypeBuilder<Ticket> builder)
    {
        builder.ToTable("Tickets").HasKey(t => t.Id);

        builder.Property(t => t.Id).HasColumnName("Id").IsRequired();
        builder.Property(t => t.EventId).HasColumnName("EventId").IsRequired();
        builder.Property(t => t.TicketType).HasColumnName("TicketType").IsRequired();
        builder.Property(t => t.Price).HasColumnName("Price").IsRequired();
        builder.Property(t => t.QuantityAvailable).HasColumnName("QuantityAvailable").IsRequired();
        builder.Property(t => t.QuantitySold).HasColumnName("QuantitySold").IsRequired();
        builder.Property(t => t.CreatedAt).HasColumnName("CreatedAt").IsRequired();
        builder.Property(t => t.UpdatedAt).HasColumnName("UpdatedAt");
        builder.Property(t => t.DeletedAt).HasColumnName("DeletedAt");

        builder.HasOne(t => t.Event)
            .WithMany(e => e.Tickets)
            .HasForeignKey(t => t.EventId);
        builder.HasMany(t => t.Attendees)
            .WithOne(a => a.Ticket)
            .HasForeignKey(a => a.TicketId);

        builder.HasQueryFilter(t => !t.DeletedAt.HasValue);
    }
}
