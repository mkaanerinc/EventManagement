using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using EventManagement.Application.Features.Attendees.Constants;
using EventManagement.Application.Services.Repositories;
using EventManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Application.Features.Attendees.Rules;

/// <summary>
/// Contains business rules related to the attendee management functionality.
/// </summary>
public class AttendeeBusinessRules : BaseBusinessRules
{
    private readonly IAttendeeRepository _attendeeRepository;
    private readonly ITicketRepository _ticketRepository;
    private readonly IEventRepository _eventRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="AttendeeBusinessRules"/> class.
    /// </summary>
    /// <param name="attendeeRepository">The attendee repository used to access attendee data.</param>
    /// <param name="ticketRepository">The ticket repository used to access ticket data.</param>
    /// <param name="eventRepository">The event repository used to access event data.</param>
    public AttendeeBusinessRules(IAttendeeRepository attendeeRepository, ITicketRepository ticketRepository, IEventRepository eventRepository)
    {
        _attendeeRepository = attendeeRepository;
        _ticketRepository = ticketRepository;
        _eventRepository = eventRepository;
    }

    /// <summary>
    /// Checks if an attendee with the specified ID exists in the repository.
    /// Throws a NotFoundException if the attendee is not found.
    /// </summary>
    /// <param name="attendeeId">The ID of the attendee to check.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    /// <exception cref="NotFoundException">Thrown when the attendee with the given ID is not found.</exception>
    public async Task CheckAttendeeExistsByIdAsync(Guid attendeeId)
    {
        bool attendeeExists = await _attendeeRepository.AnyAsync(a => a.Id == attendeeId);

        if (!attendeeExists)
        {
            throw new NotFoundException(string.Format(AttendeesMessages.NotFoundById, attendeeId));
        }
    }

    /// <summary>
    /// Ensures that the specified ticket exists in the system.
    /// </summary>
    /// <param name="ticketId">The unique identifier of the ticket.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    /// <exception cref="BusinessException">
    /// Thrown when no ticket with the given <paramref name="ticketId"/> is found.
    /// </exception>
    public async Task EnsureTicketExists(Guid ticketId)
    {
        bool exists = await _ticketRepository.AnyAsync(t => t.Id == ticketId);
        if (!exists)
            throw new BusinessException(AttendeesMessages.NotFoundTicket);
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
            throw new BusinessException(AttendeesMessages.NotFoundEvent);
    }

    /// <summary>
    /// Ensures that the specified ticket exists and has available quantity for sale.
    /// </summary>
    /// <param name="ticketId">The unique identifier of the ticket to check.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    /// <exception cref="BusinessException">
    /// Thrown when the ticket does not exist or has no available quantity.
    /// </exception>
    public async Task EnsureTicketHasAvailableQuantity(Guid ticketId)
    {
        Ticket? ticket = await _ticketRepository.GetAsync(t => t.Id == ticketId);

        if(ticket == null || ticket!.QuantityAvailable == 0)
            throw new BusinessException(AttendeesMessages.TicketNotAvailableForSale);
    }

    /// <summary>
    /// Ensures that the specified attendee has not already checked in.
    /// </summary>
    /// <param name="attendee">The attendee to check.</param>
    /// <returns>A completed task if the attendee has not checked in.</returns>
    /// <exception cref="BusinessException">
    /// Thrown when the attendee has already checked in.</exception>
    public Task EnsureAttendeeNotCheckedIn(Attendee attendee)
    {
        if (attendee.IsCheckedIn)
            throw new BusinessException(AttendeesMessages.AlreadyCheckedIn);

        return Task.CompletedTask;
    }
}