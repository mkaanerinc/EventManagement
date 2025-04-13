using Core.Infrastructure.Persistence.Repositories;
using System;

namespace EventManagement.Domain.Entities;

/// <summary>
/// Represents an attendee who has purchased a ticket for an event.
/// </summary>
public class Attendee : Entity<Guid>
{
    // To ensure null-safety at compile-time, 'required' is used. This way, these fields must be assigned, otherwise a compile-time error will occur.
    // FluentValidation ensures safety at run-time as well.
    // EF Core requires a parameterless constructor to set values to properties by fetching data from the database. For this reason, a parameterless constructor was created.
    // A parameterized constructor was also created for ease of use.

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
    /// The ticket associated with this attendee.
    /// </summary>
    /// <remarks>
    /// This navigation property corresponds to the <see cref="TicketId"/> foreign key, which is required (non-nullable) in the database schema.
    /// </remarks>
    public virtual Ticket Ticket { get; set; } = null!;

    /// <summary>
    /// Parameterless constructor required for ORM tools such as Entity Framework Core.
    /// </summary>
    public Attendee()
    {
        
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Attendee"/> class with specified details.
    /// </summary>
    /// <param name="id">The unique identifier of the attendee.</param>
    /// <param name="ticketId">The ID of the ticket associated with the attendee.</param>
    /// <param name="fullName">The full name of the attendee.</param>
    /// <param name="email">The email address of the attendee.</param>
    /// <param name="purchasedAt">The date and time when the ticket was purchased.</param>
    /// <param name="isCheckedIn">Indicates whether the attendee has checked in to the event.</param>
    public Attendee(Guid id, Guid ticketId, string fullName, string email, DateTimeOffset purchasedAt, bool isCheckedIn)
        : base(id)
    {
        TicketId = ticketId;
        FullName = fullName;
        Email = email;
        PurchasedAt = purchasedAt;
        IsCheckedIn = isCheckedIn;
    }
}
