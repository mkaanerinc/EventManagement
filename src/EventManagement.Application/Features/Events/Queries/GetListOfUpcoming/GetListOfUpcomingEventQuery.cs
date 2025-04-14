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

namespace EventManagement.Application.Features.Events.Queries.GetListOfUpcoming;

/// <summary>
/// Represents a query request for retrieving a paginated list of upcoming events occurring within the next 7 days.
/// </summary>
public class GetListOfUpcomingEventQuery : IRequest<GetListResponse<GetListOfUpcomingEventListItemDto>>
{
    /// <summary>
    /// Gets or sets the pagination parameters for the request.
    /// </summary>
    public required PageRequest PageRequest { get; set; }

    /// <summary>
    /// Handles the <see cref="GetListOfUpcomingEventQuery"/> to return a paginated list of upcoming events.
    /// </summary>
    public class GetListOfUpcomingEventQueryHandler : IRequestHandler<GetListOfUpcomingEventQuery, GetListResponse<GetListOfUpcomingEventListItemDto>>
    {
        private readonly IEventRepository _eventRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetListOfUpcomingEventQueryHandler"/> class.
        /// </summary>
        /// <param name="eventRepository">The event repository for data access.</param>
        /// <param name="mapper">The AutoMapper instance for mapping entities to DTOs.</param>
        public GetListOfUpcomingEventQueryHandler(IEventRepository eventRepository, IMapper mapper)
        {
            _eventRepository = eventRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Handles the query to retrieve a list of upcoming events occurring within the next 7 days.
        /// </summary>
        /// <param name="request">The query request containing pagination information.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A <see cref="GetListResponse{T}"/> containing a paginated list of upcoming event DTOs.</returns>
        public async Task<GetListResponse<GetListOfUpcomingEventListItemDto>> Handle(GetListOfUpcomingEventQuery request, CancellationToken cancellationToken)
        {
            Paginate<Event> events = await _eventRepository.GetListAsync(
                predicate: e => e.EventAt.Date <= DateTimeOffset.UtcNow.AddDays(7).Date,
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListOfUpcomingEventListItemDto> response = _mapper.Map<GetListResponse<GetListOfUpcomingEventListItemDto>>(events);

            return response;
        }
    }
}