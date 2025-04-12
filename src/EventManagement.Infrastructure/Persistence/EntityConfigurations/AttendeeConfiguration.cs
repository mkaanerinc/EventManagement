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
/// Configures the database schema for the Attendee entity.
/// </summary>
public class AttendeeConfiguration : IEntityTypeConfiguration<Attendee>
{
    /// <summary>
    /// Configures the Attendee entity's database mapping.
    /// </summary>
    /// <param name="builder">An instance of EntityTypeBuilder used to configure the entity.</param>
    public void Configure(EntityTypeBuilder<Attendee> builder)
    {
        builder.ToTable("Attendees").HasKey(a => a.Id);

        builder.Property(a => a.Id).HasColumnName("Id").IsRequired();
        builder.Property(a => a.TicketId).HasColumnName("TicketId").IsRequired();
        builder.Property(a => a.FullName).HasColumnName("FullName").IsRequired();
        builder.Property(a => a.Email).HasColumnName("Email").IsRequired();
        builder.Property(a => a.PurchasedAt).HasColumnName("PurchasedAt").IsRequired();
        builder.Property(a => a.IsCheckedIn).HasColumnName("IsCheckedIn").IsRequired();
        builder.Property(c => c.CreatedAt).HasColumnName("CreatedAt").IsRequired();
        builder.Property(c => c.UpdatedAt).HasColumnName("UpdatedAt");
        builder.Property(c => c.DeletedAt).HasColumnName("DeletedAt");

        builder.HasOne(a => a.Ticket)
            .WithMany(t => t.Attendees)
            .HasForeignKey(a => a.TicketId);

        builder.HasQueryFilter(a => !a.DeletedAt.HasValue);
    }
}
