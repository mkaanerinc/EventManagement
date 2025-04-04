﻿using Core.Infrastructure.Persistence.Repositories;
using System;

namespace EventManagement.Domain.Entities;

/// <summary>
/// Represents an event with details such as title, description, location, date, organizer, and capacity.
/// </summary>
public class Event : Entity<Guid>
{
    // Compile-time'da null-safety sağlamak için 'required' kullanıldı. Böylece bu alanlar mutlaka atanmalı, aksi takdirde derleme hatası alınır.
    // FluentValidation ile de run-time'da güvenlik sağlandı.
    // EF Core veritabanından verileri çekip property'lere set etmek için parametresiz constructor'a ihtiyaç duyar. Bu sebeple parametresiz constructor oluşturuldu.
    // Kullanım kolaylığı sağlamak için ise parametreli constructor oluşturuldu.

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
