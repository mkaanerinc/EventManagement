using AutoMapper;
using Core.Application.Models.Requests;
using Core.Application.Models.Responses;
using Core.Application.Rules;
using Core.Infrastructure.Persistence.Paging;
using EventManagement.Application.Features.Attendees.Queries.GetListByTicketId;
using EventManagement.Application.Features.Attendees.Rules;
using EventManagement.Application.Services.Repositories;
using EventManagement.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Application.Features.Attendees.Queries.GetListByEventId;

/// <summary>
/// Represents a query request for retrieving a paginated list of all attendees with the specified event ID.
/// </summary>
public class GetListByEventIdAttendeeQuery : IRequest<GetListResponse<GetListByEventIdAttendeeListItemDto>>
{
    /// <summary>
    /// Gets or sets the pagination parameters for the request.
    /// </summary>
    public required PageRequest PageRequest { get; set; }

    /// <summary>
    /// Gets or sets the ID of the event associated with the attendee.
    /// </summary>
    public Guid EventId { get; set; }

    /// <summary>
    /// Handles the <see cref="GetListByEventIdAttendeeQuery"/> to return a paginated list of all attendees with the specified event ID.
    /// </summary>
    public class GetListByEventIdAttendeeQueryHandler : IRequestHandler<GetListByEventIdAttendeeQuery, GetListResponse<GetListByEventIdAttendeeListItemDto>>
    {
        private readonly IAttendeeRepository _attendeeRepository;
        private readonly IMapper _mapper;
        private readonly AttendeeBusinessRules _attendeeBusinessRules;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetListByEventIdAttendeeQueryHandler"/> class.
        /// </summary>
        /// <param name="attendeeRepository">The repository used to access attendee data.</param>
        /// <param name="mapper">The AutoMapper instance used for mapping domain entities to DTOs.</param>
        /// <param name="attendeeBusinessRules">The business rules for validating attendee-specific constraints.</param>
        public GetListByEventIdAttendeeQueryHandler(IAttendeeRepository attendeeRepository, IMapper mapper, AttendeeBusinessRules attendeeBusinessRules)
        {
            _attendeeRepository = attendeeRepository;
            _mapper = mapper;
            _attendeeBusinessRules = attendeeBusinessRules;
        }

        /// <summary>
        /// Handles the query by retrieving a paginated list of all attendees with the specified event ID.
        /// </summary>
        /// <param name="request">The query request containing pagination information.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A <see cref="GetListResponse{T}"/> containing a paginated list of attendee DTOs with the specified event ID.</returns>
        /// <exception cref="BusinessException">Thrown when no attendee is found with the specified event ID.</exception>
        public async Task<GetListResponse<GetListByEventIdAttendeeListItemDto>> Handle(GetListByEventIdAttendeeQuery request, CancellationToken cancellationToken)
        {
            await RuleRunner.RunAsync(
                async () => await _attendeeBusinessRules.EnsureEventExists(request.EventId)
            );

            Paginate<Attendee> attendees = await _attendeeRepository.GetListAsync(
                predicate: a => a.Ticket.EventId == request.EventId,
                include: a => a.Include(a => a.Ticket),
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListByEventIdAttendeeListItemDto> response = _mapper.Map<GetListResponse<GetListByEventIdAttendeeListItemDto>>(attendees);

            return response;
        }
    }
}