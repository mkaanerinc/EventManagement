using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Application.Features.Tickets.Constants;

/// <summary>
/// Contains constant error and status messages related to ticket operations.
/// </summary>
public class TicketsMessages
{
    /// <summary>
    /// Message indicating that a ticket with the specified ID was not found.
    /// </summary>
    public const string NotFoundById = "Ticket with ID {0} was not found.";

    /// <summary>
    /// Message indicating that the specified event was not found.
    /// </summary>
    public const string NotFoundEvent = "Event does not exist.";

    /// <summary>
    /// Message indicating that the ticket quantity does not exceed the event's total capacity.
    /// </summary>
    public const string QuantityExceedsEventCapacity = "The available ticket quantity exceeds the event's total capacity.";

    /// <summary>
    /// Message indicating that the number of tickets sold cannot exceed the available quantity.
    /// </summary>
    public const string QuantitySoldExceedsAvailable = "The number of tickets sold cannot exceed the available quantity.";
}