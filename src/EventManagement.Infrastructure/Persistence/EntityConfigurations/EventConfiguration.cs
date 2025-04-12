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
/// Configures the database schema for the Event entity.
/// </summary>
public class EventConfiguration : IEntityTypeConfiguration<Event>
{
    /// <summary>
    /// Configures the Event entity's database mapping.
    /// </summary>
    /// <param name="builder">An instance of EntityTypeBuilder used to configure the entity.</param>
    public void Configure(EntityTypeBuilder<Event> builder)
    {
        builder.ToTable("Events").HasKey(e => e.Id);

        builder.Property(e => e.Id).HasColumnName("Id").IsRequired();
        builder.Property(e => e.Title).HasColumnName("Name").IsRequired();
        builder.Property(e => e.Description).HasColumnName("Description").IsRequired();
        builder.Property(e => e.Location).HasColumnName("Location").IsRequired();
        builder.Property(e => e.EventAt).HasColumnName("EventAt").IsRequired();
        builder.Property(e => e.OrganizerName).HasColumnName("OrganizerName").IsRequired();
        builder.Property(e => e.TotalCapacity).HasColumnName("TotalCapacity").IsRequired();
        builder.Property(e => e.CreatedAt).HasColumnName("CreatedAt").IsRequired();
        builder.Property(e => e.UpdatedAt).HasColumnName("UpdatedAt");
        builder.Property(e => e.DeletedAt).HasColumnName("DeletedAt");

        builder.HasMany(e => e.Tickets)
            .WithOne(t => t.Event)
            .HasForeignKey(t => t.EventId);
        builder.HasMany(e => e.Reports)
            .WithOne(r => r.Event)
            .HasForeignKey(r => r.EventId);

        builder.HasQueryFilter(e => !e.DeletedAt.HasValue);
    }
}
