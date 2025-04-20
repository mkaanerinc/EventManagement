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

namespace EventManagement.Application.Features.Tickets.Queries.GetById;

/// <summary>
/// Represents a query request for retrieving a specific ticket by its unique identifier.
/// </summary>
public class GetByIdTicketQuery : IRequest<GetByIdTicketResponse>
{
    /// <summary>
    /// Gets or sets the unique identifier of the ticket.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Handles the <see cref="GetByIdTicketQuery"/> to return the ticket details by its unique identifier.
    /// </summary>
    public class GetByIdTicketQueryHandler : IRequestHandler<GetByIdTicketQuery, GetByIdTicketResponse>
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IMapper _mapper;
        private readonly TicketBusinessRules _ticketBusinessRules;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetByIdTicketQueryHandler"/> class.
        /// </summary>
        /// <param name="ticketRepository">The repository used to access ticket data.</param>
        /// <param name="mapper">The AutoMapper instance used for mapping domain entities to DTOs.</param>
        /// <param name="ticketBusinessRules">The business rules for validating ticket-specific constraints.</param>
        public GetByIdTicketQueryHandler(ITicketRepository ticketRepository, IMapper mapper, TicketBusinessRules ticketBusinessRules)
        {
            _ticketRepository = ticketRepository;
            _mapper = mapper;
            _ticketBusinessRules = ticketBusinessRules;
        }

        /// <summary>
        /// Handles the query to retrieve a ticket by its unique identifier.
        /// </summary>
        /// <param name="request">The query request containing the ticket's identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A <see cref="GetByIdTicketResponse"/> containing the details of the ticket.</returns>
        /// <exception cref="NotFoundException">Thrown when no ticket is found with the specified ID.</exception>
        public async Task<GetByIdTicketResponse> Handle(GetByIdTicketQuery request, CancellationToken cancellationToken)
        {
            await RuleRunner.RunAsync(
                async () => await _ticketBusinessRules.CheckTicketExistsByIdAsync(request.Id)
            );

            Ticket? ticketbyid = await _ticketRepository.GetAsync(
                predicate: t => t.Id == request.Id,
                cancellationToken: cancellationToken
            );

            GetByIdTicketResponse response = _mapper.Map<GetByIdTicketResponse>(ticketbyid);

            return response;
        }
    }
}