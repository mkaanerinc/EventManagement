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

namespace EventManagement.Application.Features.Events.Queries.GetListByDate;

/// <summary>
/// Represents a query request for retrieving a paginated list of events that occur on a specific date.
/// </summary>
public class GetListByDateEventQuery : IRequest<GetListResponse<GetListByDateEventListItemDto>>
{
    /// <summary>
    /// Gets or sets the pagination parameters for the request.
    /// </summary>
    public required PageRequest PageRequest { get; set; }

    /// <summary>
    /// Gets or sets the specific date to filter events by.
    /// </summary>
    public DateTimeOffset EventAt { get; set; }

    /// <summary>
    /// Handles the <see cref="GetListByDateEventQuery"/> to return a paginated list of events on a specific date.
    /// </summary>
    public class GetListByDateEventQueryHandler : IRequestHandler<GetListByDateEventQuery, GetListResponse<GetListByDateEventListItemDto>>
    {
        private readonly IEventRepository _eventRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetListByDateEventQueryHandler"/> class.
        /// </summary>
        /// <param name="eventRepository">The repository used for querying event data.</param>
        /// <param name="mapper">The AutoMapper instance for mapping entities to DTOs.</param>
        public GetListByDateEventQueryHandler(IEventRepository eventRepository, IMapper mapper)
        {
            _eventRepository = eventRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Handles the query to retrieve a list of events scheduled for the specified date.
        /// </summary>
        /// <param name="request">The query request containing the target date and pagination info.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A <see cref="GetListResponse{T}"/> containing a paginated list of event DTOs for the given date.</returns>
        public async Task<GetListResponse<GetListByDateEventListItemDto>> Handle(GetListByDateEventQuery request, CancellationToken cancellationToken)
        {
            Paginate<Event> events = await _eventRepository.GetListAsync(
                predicate: e => e.EventAt.Date == request.EventAt.Date,
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListByDateEventListItemDto> response = _mapper.Map<GetListResponse<GetListByDateEventListItemDto>>(events);

            return response;
        }
    }
}