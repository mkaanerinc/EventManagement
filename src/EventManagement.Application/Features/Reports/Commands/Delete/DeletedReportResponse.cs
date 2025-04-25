using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Application.Features.Reports.Commands.Delete;

/// <summary>
/// Represents the response returned after successfully deleting a report.
/// </summary>
public class DeletedReportResponse
{
    /// <summary>
    /// Gets or sets the unique identifier of the deleted report.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the timestamp of when the report was deleted.
    /// </summary>
    public DateTimeOffset DeletedAt { get; set; }
}