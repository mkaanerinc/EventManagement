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

namespace EventManagement.Application.Features.Events.Queries.GetListByDateRange;

/// <summary>
/// Represents a query request for retrieving a paginated list of events occurring within a specified date range.
/// </summary>
public class GetListByDateRangeEventQuery : IRequest<GetListResponse<GetListByDateRangeEventListItemDto>>
{
    /// <summary>
    /// Gets or sets the pagination parameters for the request.
    /// </summary>
    public required PageRequest PageRequest { get; set; }

    /// <summary>
    /// Gets or sets the start date of the event range.
    /// </summary>
    public DateTimeOffset StartAt { get; set; }

    /// <summary>
    /// Gets or sets the end date of the event range.
    /// </summary>
    public DateTimeOffset EndAt { get; set; }

    /// <summary>
    /// Handles the <see cref="GetListByDateRangeEventQuery"/> to return a paginated list of events in the specified date range.
    /// </summary>
    public class GetListByDateRangeEventQueryHandler : IRequestHandler<GetListByDateRangeEventQuery, GetListResponse<GetListByDateRangeEventListItemDto>>
    {
        private readonly IEventRepository _eventRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetListByDateRangeEventQueryHandler"/> class.
        /// </summary>
        /// <param name="eventRepository">The repository for accessing event data.</param>
        /// <param name="mapper">The AutoMapper instance for mapping entities to DTOs.</param>
        public GetListByDateRangeEventQueryHandler(IEventRepository eventRepository, IMapper mapper)
        {
            _eventRepository = eventRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Handles the query to retrieve a list of events that occur between the specified start and end dates.
        /// </summary>
        /// <param name="request">The query request containing the date range and pagination information.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A <see cref="GetListResponse{T}"/> containing a paginated list of event DTOs within the date range.</returns>
        public async Task<GetListResponse<GetListByDateRangeEventListItemDto>> Handle(GetListByDateRangeEventQuery request, CancellationToken cancellationToken)
        {
            Paginate<Event> events = await _eventRepository.GetListAsync(
                predicate: e => e.EventAt.Date >= request.StartAt.Date && e.EventAt <= request.EndAt.Date,
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListByDateRangeEventListItemDto> response = _mapper.Map<GetListResponse<GetListByDateRangeEventListItemDto>>(events);

            return response;
        }
    }
}
