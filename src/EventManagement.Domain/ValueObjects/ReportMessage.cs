using EventManagement.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Domain.ValueObjects;

/// <summary>
/// Represents a message structure for report processing, used for RabbitMQ communication.
/// </summary>
public class ReportMessage
{
    /// <summary>
    /// Gets or sets the identifier of the report to be processed.
    /// </summary>
    public Guid ReportId { get; init; }

    /// <summary>
    /// Gets or sets the identifier of the event associated with the report.
    /// </summary>
    public Guid EventId { get; init; }

    /// <summary>
    /// Gets or sets the type of the report to be generated.
    /// </summary>
    public ReportType ReportType { get; init; }
}