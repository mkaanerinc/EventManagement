using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Application.Features.Attendees.Constants;

/// <summary>
/// Contains constant error and status messages related to attendee operations.
/// </summary>
public class AttendeesMessages
{
    /// <summary>
    /// Message indicating that an attendee with the specified ID was not found.
    /// </summary>
    public const string NotFoundById = "Attendee with ID {0} was not found.";

    /// <summary>
    /// Message indicating that the specified ticket was not found.
    /// </summary>
    public const string NotFoundTicket = "Ticket does not exist.";

    /// <summary>
    /// Message indicating that the specified event was not found.
    /// </summary>
    public const string NotFoundEvent = "Event does not exist.";

    /// <summary>
    /// Message indicating that the ticket is either not available for sale or out of stock.
    /// </summary>
    public const string TicketNotAvailableForSale = "Ticket must be available for sale and in stock.";
}