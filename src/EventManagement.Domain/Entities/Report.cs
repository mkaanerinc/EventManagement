using Core.Infrastructure.Persistence.Repositories;
using EventManagement.Domain.Enums;
using System;

namespace EventManagement.Domain.Entities;

/// <summary>
/// Represents a report generated for an event, including its type, status, and result details.
/// </summary>
public class Report : Entity<Guid>
{
    // To ensure null-safety at compile-time, 'required' is used. This way, these fields must be assigned, otherwise a compile-time error will occur.
    // FluentValidation ensures safety at run-time as well.
    // EF Core requires a parameterless constructor to set values to properties by fetching data from the database. For this reason, a parameterless constructor was created.
    // A parameterized constructor was also created for ease of use.

    /// <summary>
    /// Gets or sets the identifier of the related event.
    /// </summary>
    public Guid EventId { get; set; }

    /// <summary>
    /// Gets or sets the type of the report.
    /// </summary>
    public ReportType ReportType { get; set; }

    /// <summary>
    /// Gets or sets the result of the report generation process.
    /// </summary>
    public required string Result { get; set; }

    /// <summary>
    /// Gets or sets the current status of the report.
    /// </summary>
    public ReportStatus ReportStatus { get; set; }

    /// <summary>
    /// Gets or sets the completion timestamp of the report.
    /// </summary>
    public DateTimeOffset? CompletedAt { get; set; }

    /// <summary>
    /// The event associated with this report.
    /// </summary>
    /// <remarks>
    /// This navigation property corresponds to the <see cref="EventId"/> foreign key, which is required (non-nullable) in the database schema.
    /// </remarks>
    public virtual Event Event { get; set; } = null!;

    /// <summary>
    /// Parameterless constructor required for ORM tools such as Entity Framework Core.
    /// </summary>
    public Report()
    {
        
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Report"/> class with specified details.
    /// </summary>
    /// <param name="id">The unique identifier of the report.</param>
    /// <param name="eventId">The identifier of the related event.</param>
    /// <param name="reportType">The type of the report.</param>
    /// <param name="result">The result of the report generation process.</param>
    /// <param name="reportStatus">The current status of the report.</param>
    /// <param name="completedAt">The completion timestamp of the report.</param>
    public Report(Guid id, Guid eventId, ReportType reportType, string result, ReportStatus reportStatus, DateTimeOffset completedAt)
        : base(id)
    {
        EventId = eventId;
        ReportType = reportType;
        Result = result;
        ReportStatus = reportStatus;
        CompletedAt = completedAt;
    }
}
