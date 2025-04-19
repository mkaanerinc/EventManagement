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

namespace EventManagement.Application.Features.Tickets.Commands.Create;

/// <summary>
/// Command to create a new ticket.
/// </summary>
public class CreateTicketCommand : IRequest<CreatedTicketResponse>
{
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
    /// Handles the <see cref="CreateTicketCommand"/> to create a new ticket and return the result.
    /// </summary>
    public class CreateTicketCommandHandler : IRequestHandler<CreateTicketCommand, CreatedTicketResponse>
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IMapper _mapper;
        private readonly TicketBusinessRules _ticketBusinessRules;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateTicketCommandHandler"/> class.
        /// </summary>
        /// <param name="ticketRepository">The repository to manage ticket entities.</param>
        /// <param name="mapper">The mapper instance to convert between models.</param>
        /// <param name="ticketBusinessRules">The business rules for validating ticket-specific constraints.</param>
        public CreateTicketCommandHandler(ITicketRepository ticketRepository, IMapper mapper, TicketBusinessRules ticketBusinessRules)
        {
            _ticketRepository = ticketRepository;
            _mapper = mapper;
            _ticketBusinessRules = ticketBusinessRules;
        }

        /// <summary>
        /// Handles the creation of a new ticket.
        /// </summary>
        /// <param name="request">The create ticket command containing ticket details.</param>
        /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
        /// <returns>The response object containing details of the created ticket.</returns>
        public async Task<CreatedTicketResponse> Handle(CreateTicketCommand request, CancellationToken cancellationToken)
        {
            await RuleRunner.RunAsync(
                async () => await _ticketBusinessRules.EnsureEventExists(request.EventId),
                async () => await _ticketBusinessRules.EnsureQuantityDoesNotExceedCapacity(request.EventId, request.QuantityAvailable)
            );

            Ticket newTicket = _mapper.Map<Ticket>(request);
            newTicket.Id = Guid.NewGuid();

            await _ticketRepository.AddAsync(newTicket);

            CreatedTicketResponse response = _mapper.Map<CreatedTicketResponse>(newTicket);

            return response;
        }
    }
}
