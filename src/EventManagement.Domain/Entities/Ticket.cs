using Core.Infrastructure.Persistence.Repositories;
using EventManagement.Domain.Enums;
using System;

namespace EventManagement.Domain.Entities;

/// <summary>
/// Represents a ticket for an event, including its type, price, and availability.
/// </summary>
public class Ticket : Entity<Guid>
{
    // EF Core requires a parameterless constructor to set values to properties by fetching data from the database. For this reason, a parameterless constructor was created.
    // A parameterized constructor was also created for ease of use.

    /// <summary>
    /// Gets or sets the identifier of the event associated with this ticket.
    /// </summary>
    public Guid EventId { get; set; }

    /// <summary>
    /// Gets or sets the type of the ticket (e.g., General, Student, VIP).
    /// </summary>
    public TicketType TicketType { get; set; }

    /// <summary>
    /// Gets or sets the price of the ticket.
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Gets or sets the number of available tickets for sale.
    /// </summary>
    public int QuantityAvailable { get; set; }

    /// <summary>
    /// Gets or sets the number of tickets that have been sold.
    /// </summary>
    public int QuantitySold { get; set; }

    /// <summary>
    /// The event associated with this ticket. Every ticket must be linked to a specific event.
    /// </summary>
    /// <remarks>
    /// This navigation property corresponds to the <see cref="EventId"/> foreign key, which is required (non-nullable) in the database schema.
    /// </remarks>
    public virtual Event Event { get; set; } = null!;

    /// <summary>
    /// The collection of attendees who have purchased or are associated with this ticket.
    /// </summary>
    /// <remarks>
    /// This navigation property represents a one-to-many relationship where a single ticket can be associated with multiple attendees.
    /// The collection is initialized as an empty <see cref="HashSet{T}"/> to prevent null reference issues and ensure unique attendees.
    /// </remarks>
    public virtual ICollection<Attendee> Attendees { get; set; } = new HashSet<Attendee>();

    /// <summary>
    /// Parameterless constructor required for ORM tools such as Entity Framework Core.
    /// </summary>
    public Ticket()
    {
        
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Ticket"/> class with specified details.
    /// </summary>
    /// <param name="id">The unique identifier of the ticket.</param>
    /// <param name="eventId">The identifier of the event this ticket is for.</param>
    /// <param name="ticketType">The type of ticket (e.g., General, Student, VIP).</param>
    /// <param name="price">The price of the ticket.</param>
    /// <param name="quantityAvailable">The number of tickets available.</param>
    /// <param name="quantitySold">The number of tickets that have been sold.</param>
    public Ticket(Guid id, Guid eventId, TicketType ticketType, decimal price, int quantityAvailable, int quantitySold)
        : base(id)
    {
        EventId = eventId;
        TicketType = ticketType;
        Price = price;
        QuantityAvailable = quantityAvailable;
        QuantitySold = quantitySold;
    }
}
