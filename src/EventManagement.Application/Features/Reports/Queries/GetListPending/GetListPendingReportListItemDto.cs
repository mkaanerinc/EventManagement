using EventManagement.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Application.Features.Reports.Queries.GetListPending;

/// <summary>
/// Represents a data transfer object (DTO) for listing reports with the specified pending report status.
/// </summary>
public class GetListPendingReportListItemDto
{
    /// <summary>
    /// Gets or sets the unique identifier of the report.
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
}