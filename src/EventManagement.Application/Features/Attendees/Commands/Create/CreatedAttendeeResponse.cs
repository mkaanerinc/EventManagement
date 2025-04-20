using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Application.Features.Attendees.Commands.Create;

/// <summary>
/// Represents the response model returned after successfully creating an attendee.
/// </summary>
public class CreatedAttendeeResponse
{
    /// <summary>
    /// Gets or sets the unique identifier of the created attendee.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the ID of the ticket associated with the attendee.
    /// </summary>
    public Guid TicketId { get; set; }

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
    /// Gets or sets a value indicating whether the attendee has checked in to the event.
    /// </summary>
    public bool IsCheckedIn { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the attendee was created.
    /// </summary>
    public DateTimeOffset CreatedAt { get; set; }
}