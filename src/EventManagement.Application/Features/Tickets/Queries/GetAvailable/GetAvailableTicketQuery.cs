using AutoMapper;
using Core.Application.Models.Requests;
using Core.Application.Models.Responses;
using Core.Application.Rules;
using Core.Infrastructure.Persistence.Paging;
using EventManagement.Application.Features.Tickets.Rules;
using EventManagement.Application.Services.Repositories;
using EventManagement.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Application.Features.Tickets.Queries.GetAvailable;

/// <summary>
/// Represents a query request for retrieving available tickets for a specific event by its event ID.
/// </summary>
public class GetAvailableTicketQuery : IRequest<GetListResponse<GetAvailableTicketListItemDto>>
{
    /// <summary>
    /// Gets or sets the pagination parameters for the request.
    /// </summary>
    public required PageRequest PageRequest { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier of the event.
    /// </summary>
    public Guid EventId { get; set; }

    /// <summary>
    /// Handles the <see cref="GetAvailableTicketQuery"/> to return the list of available tickets for an event.
    /// </summary>
    public class GetAvailableTicketQueryHandler : IRequestHandler<GetAvailableTicketQuery, GetListResponse<GetAvailableTicketListItemDto>>
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IMapper _mapper;
        private readonly TicketBusinessRules _ticketBusinessRules;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetAvailableTicketQueryHandler"/> class.
        /// </summary>
        /// <param name="ticketRepository">The repository used to access ticket data.</param>
        /// <param name="mapper">The AutoMapper instance used for mapping domain entities to DTOs.</param>
        /// <param name="ticketBusinessRules">The business rules for validating ticket-specific constraints.</param>
        public GetAvailableTicketQueryHandler(ITicketRepository ticketRepository, IMapper mapper, TicketBusinessRules ticketBusinessRules)
        {
            _ticketRepository = ticketRepository;
            _mapper = mapper;
            _ticketBusinessRules = ticketBusinessRules;
        }

        /// <summary>
        /// Handles the query to retrieve available tickets for a specific event.
        /// </summary>
        /// <param name="request">The query request containing the event's identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A <see cref="GetListResponse{T}"/> containing the details of available tickets.</returns>
        /// <exception cref="NotFoundException">Thrown when no event is found with the specified ID.</exception>
        public async Task<GetListResponse<GetAvailableTicketListItemDto>> Handle(GetAvailableTicketQuery request, CancellationToken cancellationToken)
        {
            await RuleRunner.RunAsync(
                async () => await _ticketBusinessRules.EnsureEventExists(request.EventId)
            );

            Paginate<Ticket> availableTickets = await _ticketRepository.GetListAsync(
                predicate: t => t.EventId == request.EventId && t.QuantityAvailable > t.QuantitySold,
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken
            );

            GetListResponse<GetAvailableTicketListItemDto> response =  _mapper.Map<GetListResponse<GetAvailableTicketListItemDto>>(availableTickets);

            return response;
        }
    }
}