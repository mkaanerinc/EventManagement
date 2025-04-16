using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Application.Features.Events.Constants;

/// <summary>
/// Contains constant error and status messages related to event operations.
/// </summary>
public class EventsMessages
{
    /// <summary>
    /// Message indicating that an event with the specified ID was not found.
    /// </summary>
    public const string NotFoundById = "Event with ID {0} was not found.";
}
