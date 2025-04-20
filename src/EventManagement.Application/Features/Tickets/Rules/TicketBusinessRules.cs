using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using EventManagement.Application.Features.Events.Constants;
using EventManagement.Application.Features.Tickets.Constants;
using EventManagement.Application.Services.Repositories;
using EventManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Application.Features.Tickets.Rules;

/// <summary>
/// Contains business rules related to the ticket management functionality.
/// </summary>
public class TicketBusinessRules : BaseBusinessRules
{
    private readonly ITicketRepository _ticketRepository;
    private readonly IEventRepository _eventRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="TicketBusinessRules"/> class.
    /// </summary>
    /// <param name="ticketRepository">The ticket repository used to access ticket data.</param>
    /// <param name="eventRepository">The event repository used to access event data.</param>
    public TicketBusinessRules(ITicketRepository ticketRepository, IEventRepository eventRepository)
    {
        _ticketRepository = ticketRepository;
        _eventRepository = eventRepository;
    }

    /// <summary>
    /// Checks if a ticket with the specified ID exists in the repository.
    /// Throws a NotFoundException if the ticket is not found.
    /// </summary>
    /// <param name="ticketId">The ID of the ticket to check.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    /// <exception cref="NotFoundException">Thrown when the ticket with the given ID is not found.</exception>
    public async Task CheckTicketExistsByIdAsync(Guid ticketId)
    {
        bool ticketExists = await _ticketRepository.AnyAsync(t => t.Id == ticketId);

        if (!ticketExists)
        {
            throw new NotFoundException(string.Format(TicketsMessages.NotFoundById, ticketId));
        }
    }

    /// <summary>
    /// Ensures that the number of tickets sold does not exceed the available quantity.
    /// </summary>
    /// <param name="quantitySold">The number of tickets that have been sold.</param>
    /// <param name="quantityAvailable">The number of tickets available for sale.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    /// <exception cref="BusinessException">
    /// Thrown when <paramref name="quantitySold"/> exceeds <paramref name="quantityAvailable"/>.
    /// </exception>
    public Task EnsureQuantitySoldIsValid(int quantitySold, int quantityAvailable)
    {
        if (quantitySold > quantityAvailable)
            throw new BusinessException(TicketsMessages.QuantitySoldExceedsAvailable);

        return Task.CompletedTask;
    }

    /// <summary>
    /// Ensures that the specified event exists in the system.
    /// </summary>
    /// <param name="eventId">The unique identifier of the event.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    /// <exception cref="BusinessException">
    /// Thrown when no event with the given <paramref name="eventId"/> is found.
    /// </exception>
    public async Task EnsureEventExists(Guid eventId)
    {
        var exists = await _eventRepository.AnyAsync(e => e.Id == eventId);
        if (!exists)
            throw new BusinessException(TicketsMessages.NotFoundEvent);
    }

    /// <summary>
    /// Ensures that the quantity of tickets does not exceed the total capacity of the event.
    /// </summary>
    /// <param name="eventId">The unique identifier of the event.</param>
    /// <param name="quantityAvailable">The number of tickets available for the event.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    /// <exception cref="BusinessException">
    /// Thrown when <paramref name="quantityAvailable"/> exceeds the event's total capacity.
    /// </exception>
    public async Task EnsureQuantityDoesNotExceedCapacity(Guid eventId, int quantityAvailable)
    {
        Event? eventInfo = await _eventRepository.GetAsync(e => e.Id == eventId);

        if(quantityAvailable > eventInfo!.TotalCapacity)
            throw new BusinessException(TicketsMessages.QuantityExceedsEventCapacity);
    }
}