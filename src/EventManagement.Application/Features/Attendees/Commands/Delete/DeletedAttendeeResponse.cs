using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Application.Features.Attendees.Commands.Delete;

/// <summary>
/// Represents the response returned after successfully deleting an attendee.
/// </summary>
public class DeletedAttendeeResponse
{
    /// <summary>
    /// Gets or sets the unique identifier of the deleted attendee.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the timestamp of when the attendee was deleted.
    /// </summary>
    public DateTimeOffset DeletedAt { get; set; }
}