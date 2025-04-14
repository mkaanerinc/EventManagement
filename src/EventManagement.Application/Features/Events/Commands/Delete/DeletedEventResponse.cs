using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Application.Features.Events.Commands.Delete;

/// <summary>
/// Represents the response returned after successfully deleting an event.
/// </summary>
public class DeletedEventResponse
{
    /// <summary>
    /// Gets or sets the unique identifier of the deleted event.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the timestamp of when the event was deleted.
    /// </summary>
    public DateTimeOffset DeletedAt { get; set; }
}
