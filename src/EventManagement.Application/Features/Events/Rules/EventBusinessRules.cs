using Core.Application.Rules;
using EventManagement.Application.Services.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Application.Features.Events.Rules;

/// <summary>
/// Contains business rules related to the event management functionality.
/// </summary>
public class EventBusinessRules : BaseBusinessRules
{
    private readonly IEventRepository _eventRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="EventBusinessRules"/> class.
    /// </summary>
    /// <param name="eventRepository">The event repository used to access event data.</param>
    public EventBusinessRules(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }
}
