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

namespace EventManagement.Application.Features.Events.Queries.GetList;

/// <summary>
/// Represents a query request for retrieving a paginated list of all events.
/// </summary>
public class GetListEventQuery : IRequest<GetListResponse<GetListEventListItemDto>>
{
    /// <summary>
    /// Gets or sets the pagination parameters for the request.
    /// </summary>
    public required PageRequest PageRequest { get; set; }

    /// <summary>
    /// Handles the <see cref="GetListEventQuery"/> to return a paginated list of all events.
    /// </summary>
    public class GetListEventQueryHandler : IRequestHandler<GetListEventQuery, GetListResponse<GetListEventListItemDto>>
    {
        private readonly IEventRepository _eventRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetListEventQueryHandler"/> class.
        /// </summary>
        /// <param name="eventRepository">The repository used to access event data.</param>
        /// <param name="mapper">The AutoMapper instance used for mapping domain entities to DTOs.</param>
        public GetListEventQueryHandler(IEventRepository eventRepository, IMapper mapper)
        {
            _eventRepository = eventRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Handles the query by retrieving a paginated list of all events.
        /// </summary>
        /// <param name="request">The query request containing pagination information.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A <see cref="GetListResponse{T}"/> containing a paginated list of event DTOs.</returns>
        public async Task<GetListResponse<GetListEventListItemDto>> Handle(GetListEventQuery request, CancellationToken cancellationToken)
        {
            Paginate<Event> events = await _eventRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListEventListItemDto> response = _mapper.Map<GetListResponse<GetListEventListItemDto>>(events);

            return response;
        }
    }
}