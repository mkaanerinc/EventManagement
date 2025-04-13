using Core.Infrastructure.Persistence.Repositories;
using System;

namespace EventManagement.Domain.Entities;

/// <summary>
/// Represents an event with details such as title, description, location, date, organizer, and capacity.
/// </summary>
public class Event : Entity<Guid>
{
    // To ensure null-safety at compile-time, 'required' is used. This way, these fields must be assigned, otherwise a compile-time error will occur.
    // FluentValidation ensures safety at run-time as well.
    // EF Core requires a parameterless constructor to set values to properties by fetching data from the database. For this reason, a parameterless constructor was created.
    // A parameterized constructor was also created for ease of use.

    /// <summary>
    /// Gets or sets the title of the event.
    /// </summary>
    public required string Title { get; set; }

    /// <summary>
    /// Gets or sets the description of the event.
    /// </summary>
    public required string Description { get; set; }

    /// <summary>
    /// Gets or sets the location where the event will be held.
    /// </summary>
    public required string Location { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the event will take place.
    /// </summary>
    public DateTimeOffset EventAt { get; set; }

    /// <summary>
    /// Gets or sets the name of the event organizer.
    /// </summary>
    public required string OrganizerName { get; set; }

    /// <summary>
    /// Gets or sets the total capacity of the event.
    /// </summary>
    public int TotalCapacity { get; set; }

    /// <summary>
    /// The collection of tickets associated with this event.
    /// </summary>
    /// <remarks>
    /// This navigation property represents a one-to-many relationship where a single event can be associated with multiple tickets.
    /// The collection is initialized as an empty <see cref="HashSet{T}"/> to prevent null reference issues and ensure unique tickets.
    /// </remarks>
    public virtual ICollection<Ticket> Tickets { get; set; } = new HashSet<Ticket>();

    /// <summary>
    /// The collection of reports associated with this event.
    /// </summary>
    /// <remarks>
    /// This navigation property represents a one-to-many relationship where a single event can be associated with multiple reports.
    /// The collection is initialized as an empty <see cref="HashSet{T}"/> to prevent null reference issues and ensure unique reports.
    /// </remarks>
    public virtual ICollection<Report> Reports { get; set; } = new HashSet<Report>();

    /// <summary>
    /// Parameterless constructor required for ORM tools such as Entity Framework Core.
    /// </summary>
    public Event()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Event"/> class with specified details.
    /// </summary>
    /// <param name="id">The unique identifier of the event.</param>
    /// <param name="title">The title of the event.</param>
    /// <param name="description">The description of the event.</param>
    /// <param name="location">The location where the event will be held.</param>
    /// <param name="eventAt">The date and time of the event.</param>
    /// <param name="organizerName">The name of the organizer.</param>
    /// <param name="totalCapacity">The maximum number of attendees for the event.</param>
    public Event(Guid id, string title, string description, string location, DateTimeOffset eventAt, string organizerName, int totalCapacity)
        : base(id)
    {
        Title = title;
        Description = description;
        Location = location;
        EventAt = eventAt;
        OrganizerName = organizerName;
        TotalCapacity = totalCapacity;
    }
}
