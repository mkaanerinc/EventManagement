using AutoMapper;
using Core.Application.Models.Requests;
using Core.Application.Models.Responses;
using Core.Application.Rules;
using Core.Infrastructure.Persistence.Paging;
using EventManagement.Application.Features.Tickets.Rules;
using EventManagement.Application.Services.Repositories;
using EventManagement.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Application.Features.Tickets.Queries.GetListByEventId;

/// <summary>
/// Represents a query request for retrieving a paginated list of all tickets with the specified event ID.
/// </summary>
public class GetListByEventIdTicketQuery : IRequest<GetListResponse<GetListByEventIdTicketListItemDto>>
{
    /// <summary>
    /// Gets or sets the pagination parameters for the request.
    /// </summary>
    public required PageRequest PageRequest { get; set; }

    /// <summary>
    /// Gets or sets the ID of the event associated with the ticket.
    /// </summary>
    public Guid EventId { get; set; }

    /// <summary>
    /// Handles the <see cref="GetListByEventIdTicketQuery"/> to return a paginated list of all tickets with the specified event ID.
    /// </summary>
    public class GetListByEventIdTicketQueryHandler : IRequestHandler<GetListByEventIdTicketQuery, GetListResponse<GetListByEventIdTicketListItemDto>>
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IMapper _mapper;
        private readonly TicketBusinessRules _ticketBusinessRules;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetListByEventIdTicketQueryHandler"/> class.
        /// </summary>
        /// <param name="ticketRepository">The repository used to access ticket data.</param>
        /// <param name="mapper">The AutoMapper instance used for mapping domain entities to DTOs.</param>
        /// <param name="ticketBusinessRules">The business rules for validating ticket-specific constraints.</param>
        public GetListByEventIdTicketQueryHandler(ITicketRepository ticketRepository, IMapper mapper, TicketBusinessRules ticketBusinessRules)
        {
            _ticketRepository = ticketRepository;
            _mapper = mapper;
            _ticketBusinessRules = ticketBusinessRules;
        }

        /// <summary>
        /// Handles the query by retrieving a paginated list of all tickets with the specified event ID.
        /// </summary>
        /// <param name="request">The query request containing pagination information.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A <see cref="GetListResponse{T}"/> containing a paginated list of ticket DTOs with the specified event ID.</returns>
        /// <exception cref="BusinessException">Thrown when no ticket is found with the specified event ID.</exception>
        public async Task<GetListResponse<GetListByEventIdTicketListItemDto>> Handle(GetListByEventIdTicketQuery request, CancellationToken cancellationToken)
        {
            await RuleRunner.RunAsync(
                async () => await _ticketBusinessRules.EnsureEventExists(request.EventId)
            );

            Paginate<Ticket> tickets = await _ticketRepository.GetListAsync(
                predicate: t => t.EventId == request.EventId,
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListByEventIdTicketListItemDto> response = _mapper.Map<GetListResponse<GetListByEventIdTicketListItemDto>>(tickets);

            return response;
        }
    }
}
