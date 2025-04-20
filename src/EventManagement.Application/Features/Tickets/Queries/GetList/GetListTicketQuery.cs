using AutoMapper;
using Core.Application.Models.Requests;
using Core.Application.Models.Responses;
using Core.Infrastructure.Persistence.Paging;
using EventManagement.Application.Services.Repositories;
using EventManagement.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Application.Features.Tickets.Queries.GetList;

/// <summary>
/// Represents a query request for retrieving a paginated list of all tickets.
/// </summary>
public class GetListTicketQuery : IRequest<GetListResponse<GetListTicketListItemDto>>
{
    /// <summary>
    /// Gets or sets the pagination parameters for the request.
    /// </summary>
    public required PageRequest PageRequest { get; set; }

    /// <summary>
    /// Handles the <see cref="GetListTicketQuery"/> to return a paginated list of all tickets.
    /// </summary>
    public class GetListTicketQueryHandler : IRequestHandler<GetListTicketQuery, GetListResponse<GetListTicketListItemDto>>
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetListTicketQueryHandler"/> class.
        /// </summary>
        /// <param name="ticketRepository">The repository used to access ticket data.</param>
        /// <param name="mapper">The AutoMapper instance used for mapping domain entities to DTOs.</param>
        public GetListTicketQueryHandler(ITicketRepository ticketRepository, IMapper mapper)
        {
            _ticketRepository = ticketRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Handles the query by retrieving a paginated list of all tickets.
        /// </summary>
        /// <param name="request">The query request containing pagination information.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A <see cref="GetListResponse{T}"/> containing a paginated list of ticket DTOs.</returns>
        public async Task<GetListResponse<GetListTicketListItemDto>> Handle(GetListTicketQuery request, CancellationToken cancellationToken)
        {
            Paginate<Ticket> tickets = await _ticketRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListTicketListItemDto> response = _mapper.Map<GetListResponse<GetListTicketListItemDto>>(tickets);

            return response;
        }
    }
}
