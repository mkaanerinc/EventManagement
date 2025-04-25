using EventManagement.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Application.Features.Reports.Commands.Update;

/// <summary>
/// Represents the response model returned after successfully updating a report.
/// </summary>
public class UpdatedReportResponse
{
    /// <summary>
    /// Gets or sets the unique identifier of the created report.
    /// </summary>
    public Guid Id { get; set; }

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
    /// Gets or sets the date and time when the ticket was created.
    /// </summary>
    public DateTimeOffset CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets the timestamp of the last update to the entity.
    /// </summary>
    public DateTimeOffset UpdatedAt { get; set; }
}