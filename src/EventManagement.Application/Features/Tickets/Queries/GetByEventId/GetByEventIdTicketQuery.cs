using AutoMapper;
using Core.Application.Rules;
using EventManagement.Application.Features.Tickets.Rules;
using EventManagement.Application.Services.Repositories;
using EventManagement.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Application.Features.Tickets.Queries.GetByEventId;

/// <summary>
/// Represents a query request for retrieving a specific ticket by its event unique identifier.
/// </summary>
public class GetByEventIdTicketQuery : IRequest<GetByEventIdTicketResponse>
{
    /// <summary>
    /// Gets or sets the identifier of the event associated with this ticket.
    /// </summary>
    public Guid EventId { get; set; }

    /// <summary>
    /// Handles the <see cref="GetByEventIdTicketQuery"/> to return the ticket details by its event unique identifier.
    /// </summary>
    public class GetByEventIdTicketQueryHandler : IRequestHandler<GetByEventIdTicketQuery, GetByEventIdTicketResponse>
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IMapper _mapper;
        private readonly TicketBusinessRules _ticketBusinessRules;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetByEventIdTicketQueryHandler"/> class.
        /// </summary>
        /// <param name="ticketRepository">The repository used to access ticket data.</param>
        /// <param name="mapper">The AutoMapper instance used for mapping domain entities to DTOs.</param>
        /// <param name="ticketBusinessRules">The business rules for validating ticket-specific constraints.</param>
        public GetByEventIdTicketQueryHandler(ITicketRepository ticketRepository, IMapper mapper, TicketBusinessRules ticketBusinessRules)
        {
            _ticketRepository = ticketRepository;
            _mapper = mapper;
            _ticketBusinessRules = ticketBusinessRules;
        }

        /// <summary>
        /// Handles the query to retrieve a ticket by its event unique identifier.
        /// </summary>
        /// <param name="request">The query request containing the ticket's identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A <see cref="GetByEventIdTicketResponse"/> containing the details of the ticket.</returns>
        /// <exception cref="NotFoundException">Thrown when no ticket is found with the specified event ID.</exception>
        public async Task<GetByEventIdTicketResponse> Handle(GetByEventIdTicketQuery request, CancellationToken cancellationToken)
        {
            await RuleRunner.RunAsync(
                async () => await _ticketBusinessRules.CheckEventExistsByIdAsync(request.EventId)         
            );

            Ticket? ticketbyeventid = await _ticketRepository.GetAsync(
                predicate: t => t.EventId == request.EventId,
                cancellationToken: cancellationToken
            );

            GetByEventIdTicketResponse response = _mapper.Map<GetByEventIdTicketResponse>(ticketbyeventid);

            return response;
        }
    }
}
