using Core.Application.Rules;
using EventManagement.Application.Features.Tickets.Constants;
using EventManagement.Application.Features.Tickets.Rules;
using EventManagement.Application.Services.Repositories;
using EventManagement.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Application.Features.Tickets.Commands.SellTicket;

/// <summary>
/// Command to sell a ticket.
/// </summary>
public class SellTicketCommand : IRequest<SoldTicketResponse>
{
    /// <summary>
    /// Gets or sets the ID of the ticket.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the full name of the attendee.
    /// </summary>
    public required string FullName { get; set; }

    /// <summary>
    /// Gets or sets the email address of the attendee.
    /// </summary>
    public required string Email { get; set; }

    /// <summary>
    /// Handles the <see cref="SellTicketCommand"/> to sell a ticket and return the result.
    /// </summary>
    public class SellTicketCommandHandler : IRequestHandler<SellTicketCommand, SoldTicketResponse>
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IAttendeeRepository _attendeeRepository;
        private readonly TicketBusinessRules _ticketBusinessRules;

        /// <summary>
        /// Initializes a new instance of the <see cref="SellTicketCommandHandler"/> class.
        /// </summary>
        /// <param name="ticketRepository">The repository to manage ticket entities.</param>
        /// <param name="attendeeRepository">The repository to manage attendee entities.</param>
        /// <param name="ticketBusinessRules">The business rules for validating ticket-specific constraints.</param>
        public SellTicketCommandHandler(ITicketRepository ticketRepository, TicketBusinessRules ticketBusinessRules, IAttendeeRepository attendeeRepository)
        {
            _ticketRepository = ticketRepository;
            _ticketBusinessRules = ticketBusinessRules;
            _attendeeRepository = attendeeRepository;
        }

        /// <summary>
        /// Handles the sell of a ticket.
        /// </summary>
        /// <param name="request">The sell ticket command containing ticket ID and attendee details.</param>
        /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
        /// <returns>The response object containing details of the sold ticket.</returns>
        /// <exception cref="NotFoundException">Thrown when no ticket is found with the specified ticket ID.</exception>
        public async Task<SoldTicketResponse> Handle(SellTicketCommand request, CancellationToken cancellationToken)
        {
            await RuleRunner.RunAsync(
            async () => await _ticketBusinessRules.CheckTicketExistsByIdAsync(request.Id)
            );

            Ticket? ticket = await _ticketRepository.GetAsync(
                predicate: t => t.Id == request.Id,
                cancellationToken: cancellationToken
            );

            await RuleRunner.RunAsync(
            async () => await _ticketBusinessRules.EnsureQuantitySoldIsValid(ticket!.QuantitySold, ticket!.QuantityAvailable),
            async () => await _ticketBusinessRules.EnsureQuantityDoesNotExceedCapacity(ticket!.EventId, ticket!.QuantityAvailable)
            );

            ticket!.QuantitySold += 1;
            ticket!.UpdatedAt = DateTimeOffset.UtcNow;
            await _ticketRepository.UpdateAsync(ticket);

            Attendee attendee = new()
            {
                TicketId = request.Id,
                FullName = request.FullName,
                Email = request.Email,
                PurchasedAt = DateTimeOffset.UtcNow,
                IsCheckedIn = false
            };

            await _attendeeRepository.AddAsync(attendee);

            SoldTicketResponse response = new()
            {
                AttendeeId = attendee.Id,
                FullName = attendee.FullName,
                Email = attendee.Email,
                PurchasedAt = attendee.PurchasedAt,
                TicketId = ticket.Id,
                TicketType = ticket.TicketType,
                Message = TicketsMessages.TicketSoldSuccessfully
            };

            return response;
        }
    }
}