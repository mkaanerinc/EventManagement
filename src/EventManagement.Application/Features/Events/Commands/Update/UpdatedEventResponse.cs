using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Application.Features.Events.Commands.Update;

public class UpdatedEventResponse
{
    /// <summary>
    /// Gets or sets the unique identifier of the updated event.
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
    /// Gets or sets the location where the event will take place.
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
    /// Gets or sets the date and time when the event was created.
    /// </summary>
    public DateTimeOffset CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets the timestamp of the last update to the entity.
    /// </summary>
    public DateTimeOffset UpdatedAt { get; set; }
}
