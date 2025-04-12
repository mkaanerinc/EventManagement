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
/// Configures the database schema for the Report entity.
/// </summary>
public class ReportConfiguration : IEntityTypeConfiguration<Report>
{
    // <summary>
    /// Configures the Report entity's database mapping.
    /// </summary>
    /// <param name="builder">An instance of EntityTypeBuilder used to configure the entity.</param>
    public void Configure(EntityTypeBuilder<Report> builder)
    {
        builder.ToTable("Reports").HasKey(r => r.Id);

        builder.Property(r => r.Id).HasColumnName("Id").IsRequired();
        builder.Property(r => r.EventId).HasColumnName("EventId").IsRequired();
        builder.Property(r => r.ReportType).HasColumnName("ReportType").IsRequired();
        builder.Property(r => r.Result).HasColumnName("Result").IsRequired();
        builder.Property(r => r.ReportStatus).HasColumnName("ReportStatus").IsRequired();
        builder.Property(r => r.CompletedAt).HasColumnName("CompletedAt");
        builder.Property(r => r.CreatedAt).HasColumnName("CreatedAt").IsRequired();
        builder.Property(r => r.UpdatedAt).HasColumnName("UpdatedAt");
        builder.Property(r => r.DeletedAt).HasColumnName("DeletedAt");

        builder.HasOne(r => r.Event)
            .WithMany(e => e.Reports)
            .HasForeignKey(r => r.EventId);

        builder.HasQueryFilter(r => !r.DeletedAt.HasValue);
    }
}
