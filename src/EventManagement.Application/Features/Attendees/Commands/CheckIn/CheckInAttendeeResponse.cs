using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Application.Features.Attendees.Commands.CheckIn;

/// <summary>
/// Represents the response model returned after successfully updating attendee's check-in.
/// </summary>
public class CheckInAttendeeResponse
{
    /// <summary>
    /// Gets or sets a value indicating whether the attendee has checked in to the event.
    /// </summary>
    public bool IsCheckedIn { get; set; }

    /// <summary>
    /// Gets or sets a message describing the result of the check-in operation.
    /// </summary>
    public required string Message { get; set; }
}