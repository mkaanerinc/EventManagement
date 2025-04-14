using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Application.Features.Events.Queries.GetRemainingTicketCount;

/// <summary>
/// Represents the response containing the remaining ticket count for an event.
/// </summary>
public class GetRemainingTicketCountEventResponse
{
    /// <summary>
    /// Gets or sets the remaining ticket count.
    /// </summary>
    public int RemainingTicketCount { get; set; }
}
