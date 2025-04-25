using AutoMapper;
using Core.Application.Rules;
using EventManagement.Application.Features.Tickets.Rules;
using EventManagement.Application.Services.Repositories;
using EventManagement.Domain.Entities;
using EventManagement.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Application.Features.Tickets.Commands.Update;

/// <summary>
/// Command to update a ticket.
/// </summary>
public class UpdateTicketCommand : IRequest<UpdatedTicketResponse>
{
    /// <summary>
    /// Gets or sets the unique identifier of the updated ticket.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the event associated with this ticket.
    /// </summary>
    public Guid EventId { get; set; }

    /// <summary>
    /// Gets or sets the type of the ticket (e.g., General, Student, VIP).
    /// </summary>
    public TicketType TicketType { get; set; }

    /// <summary>
    /// Gets or sets the price of the ticket.
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Gets or sets the number of available tickets for sale.
    /// </summary>
    public int QuantityAvailable { get; set; }

    /// <summary>
    /// Gets or sets the number of tickets that have been sold.
    /// </summary>
    public int QuantitySold { get; set; }

    /// <summary>
    /// Handles the <see cref="UpdateTicketCommand"/> to update a ticket and return the result.
    /// </summary>
    public class UpdateTicketCommandHandler : IRequestHandler<UpdateTicketCommand, UpdatedTicketResponse>
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IMapper _mapper;
        private readonly TicketBusinessRules _ticketBusinessRules;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateTicketCommandHandler"/> class.
        /// </summary>
        /// <param name="ticketRepository">The repository to manage ticket entities.</param>
        /// <param name="mapper">The mapper instance to convert between models.</param>
        /// <param name="ticketBusinessRules">The business rules for validating ticket-specific constraints.</param>
        public UpdateTicketCommandHandler(ITicketRepository ticketRepository, IMapper mapper, TicketBusinessRules ticketBusinessRules)
        {
            _ticketRepository = ticketRepository;
            _mapper = mapper;
            _ticketBusinessRules = ticketBusinessRules;
        }

        /// <summary>
        /// Handles the update of a ticket.
        /// </summary>
        /// <param name="request">The update ticket command containing ticket details.</param>
        /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
        /// <returns>The response object containing details of the updated ticket.</returns>
        /// <exception cref="NotFoundException">Thrown when no ticket is found with the specified ticket ID.</exception>
        /// <exception cref="BusinessException">Thrown when no event is found with the specified event ID.</exception>
        /// <exception cref="BusinessException">Thrown when quantityAvailable exceeds the event's total capacity.</exception>
        /// <exception cref="BusinessException">Thrown when quantitySold exceeds quantityAvailable.</exception>
        public async Task<UpdatedTicketResponse> Handle(UpdateTicketCommand request, CancellationToken cancellationToken)
        {
            await RuleRunner.RunAsync(
                async () => await _ticketBusinessRules.CheckTicketExistsByIdAsync(request.Id),
                async () => await _ticketBusinessRules.EnsureEventExists(request.EventId),
                async () => await _ticketBusinessRules.EnsureQuantityDoesNotExceedCapacity(request.EventId, request.QuantityAvailable),
                async () => await _ticketBusinessRules.EnsureQuantitySoldIsValid(request.QuantitySold, request.QuantityAvailable)
            );

            Ticket? updatedTicket = await _ticketRepository.GetAsync
                (predicate: t => t.Id == request.Id,
                cancellationToken: cancellationToken
                );

            updatedTicket = _mapper.Map(request, updatedTicket);

            await _ticketRepository.UpdateAsync(updatedTicket!);

            UpdatedTicketResponse response = _mapper.Map<UpdatedTicketResponse>(updatedTicket);

            return response;
        }
    }
}
