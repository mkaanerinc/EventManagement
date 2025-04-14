using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Application.Features.Events.Queries.GetListOfUpcoming;

/// <summary>
/// Represents a data transfer object (DTO) for listing events filtered by upcoming dates.
/// </summary>
public class GetListOfUpcomingEventListItemDto
{
    /// <summary>
    /// Gets or sets the unique identifier of the event.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the title of the event.
    /// </summary>
    public required string Title { get; set; }

    /// <summary>
    /// Gets or sets the description of the event.
    /// </summary>
    public required string Description { get; set; }

    /// <summary>
    /// Gets or sets the location where the event is held.
    /// </summary>
    public required string Location { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the event takes place.
    /// </summary>
    public DateTimeOffset EventAt { get; set; }

    /// <summary>
    /// Gets or sets the name of the organizer of the event.
    /// </summary>
    public required string OrganizerName { get; set; }

    /// <summary>
    /// Gets or sets the total capacity of attendees for the event.
    /// </summary>
    public int TotalCapacity { get; set; }
}
