using EventManagement.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Application.Features.Tickets.Commands.SellTicket;

/// <summary>
/// Represents the response for a successful ticket sale operation.
/// </summary>
public class SoldTicketResponse
{
    /// <summary>
    /// Gets or sets the ID of the attendee.
    /// </summary>
    public Guid AttendeeId { get; set; }

    /// <summary>
    /// Gets or sets the full name of the attendee.
    /// </summary>
    public required string FullName { get; set; }

    /// <summary>
    /// Gets or sets the email address of the attendee.
    /// </summary>
    public required string Email { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the ticket was purchased.
    /// </summary>
    public DateTimeOffset PurchasedAt { get; set; }

    /// <summary>
    /// Gets or sets the ID of the ticket.
    /// </summary>
    public Guid TicketId { get; set; }

    /// <summary>
    /// Gets or sets the type of the ticket (e.g., General, Student, VIP).
    /// </summary>
    public TicketType TicketType { get; set; }

    /// <summary>
    /// Gets or sets a message describing the result of the sell ticket operation.
    /// </summary>
    public required string Message { get; set; }
}