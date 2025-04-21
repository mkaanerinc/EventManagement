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

namespace EventManagement.Application.Features.Attendees.Queries.GetList;

/// <summary>
/// Represents a query request for retrieving a paginated list of all attendees.
/// </summary>
public class GetListAttendeeQuery : IRequest<GetListResponse<GetListAttendeeListItemDto>>
{
    /// <summary>
    /// Gets or sets the pagination parameters for the request.
    /// </summary>
    public required PageRequest PageRequest { get; set; }

    /// <summary>
    /// Handles the <see cref="GetListAttendeeQuery"/> to return a paginated list of all attendees.
    /// </summary>
    public class GetListAttendeeQueryHandler : IRequestHandler<GetListAttendeeQuery, GetListResponse<GetListAttendeeListItemDto>>
    {
        private readonly IAttendeeRepository _attendeeRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetListAttendeeQueryHandler"/> class.
        /// </summary>
        /// <param name="attendeeRepository">The repository used to access attendee data.</param>
        /// <param name="mapper">The AutoMapper instance used for mapping domain entities to DTOs.</param>
        public GetListAttendeeQueryHandler(IAttendeeRepository attendeeRepository, IMapper mapper)
        {
            _attendeeRepository = attendeeRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Handles the query by retrieving a paginated list of all attendees.
        /// </summary>
        /// <param name="request">The query request containing pagination information.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A <see cref="GetListResponse{T}"/> containing a paginated list of attendee DTOs.</returns>
        public async Task<GetListResponse<GetListAttendeeListItemDto>> Handle(GetListAttendeeQuery request, CancellationToken cancellationToken)
        {
            Paginate<Attendee> attendees = await _attendeeRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListAttendeeListItemDto> response = _mapper.Map<GetListResponse<GetListAttendeeListItemDto>>(attendees);

            return response;
        }
    }
}