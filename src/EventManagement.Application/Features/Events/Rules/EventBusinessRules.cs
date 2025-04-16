using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using EventManagement.Application.Features.Events.Constants;
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

    /// <summary>
    /// Checks if an event with the specified ID exists in the repository.
    /// Throws a NotFoundException if the event is not found.
    /// </summary>
    /// <param name="eventId">The ID of the event to check.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    /// <exception cref="NotFoundException">Thrown when the event with the given ID is not found.</exception>
    public async Task CheckEventExistsByIdAsync(Guid eventId)
    {
        bool eventExists = await _eventRepository.AnyAsync(e => e.Id == eventId);

        if (!eventExists)
        {
            throw new NotFoundException(string.Format(EventsMessages.NotFoundById, eventId));
        }
    }
}
