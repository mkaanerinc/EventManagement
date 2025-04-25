using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Application.Features.Reports.Constants;

/// <summary>
/// Contains constant error and status messages related to report operations.
/// </summary>
public class ReportsMessages
{
    /// <summary>
    /// Message indicating that a report with the specified ID was not found.
    /// </summary>
    public const string NotFoundById = "Report with ID {0} was not found.";

    /// <summary>
    /// Message indicating that the specified event was not found.
    /// </summary>
    public const string NotFoundEvent = "Event does not exist.";
}