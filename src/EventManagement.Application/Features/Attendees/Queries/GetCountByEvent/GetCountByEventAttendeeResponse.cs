using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Application.Features.Attendees.Queries.GetCountByEvent;

/// <summary>
/// Represents the response containing the total count of attendees with specified event.
/// </summary>
public class GetCountByEventAttendeeResponse
{
    /// <summary>
    /// Gets or sets the total count of attendees.
    /// </summary>
    public int TotalCountOfAttendees { get; set; }
}