using AutoMapper;
using Core.Application.Models.Requests;
using Core.Application.Models.Responses;
using Core.Application.Rules;
using Core.Infrastructure.Persistence.Paging;
using EventManagement.Application.Features.Attendees.Rules;
using EventManagement.Application.Services.Repositories;
using EventManagement.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Application.Features.Attendees.Queries.GetListByTicketId;

/// <summary>
/// Represents a query request for retrieving a paginated list of all attendees with the specified ticket ID.
/// </summary>
public class GetListByTicketIdAttendeeQuery : IRequest<GetListResponse<GetListByTicketIdAttendeeListItemDto>>
{
    /// <summary>
    /// Gets or sets the pagination parameters for the request.
    /// </summary>
    public required PageRequest PageRequest { get; set; }

    /// <summary>
    /// Gets or sets the ID of the ticket associated with the attendee.
    /// </summary>
    public Guid TicketId { get; set; }

    /// <summary>
    /// Handles the <see cref="GetListByTicketIdAttendeeQuery"/> to return a paginated list of all attendees with the specified ticket ID.
    /// </summary>
    public class GetListByTicketIdAttendeeQueryHandler : IRequestHandler<GetListByTicketIdAttendeeQuery, GetListResponse<GetListByTicketIdAttendeeListItemDto>>
    {
        private readonly IAttendeeRepository _attendeeRepository;
        private readonly IMapper _mapper;
        private readonly AttendeeBusinessRules _attendeeBusinessRules;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetListByTicketIdAttendeeQueryHandler"/> class.
        /// </summary>
        /// <param name="attendeeRepository">The repository used to access attendee data.</param>
        /// <param name="mapper">The AutoMapper instance used for mapping domain entities to DTOs.</param>
        /// <param name="attendeeBusinessRules">The business rules for validating attendee-specific constraints.</param>
        public GetListByTicketIdAttendeeQueryHandler(IAttendeeRepository attendeeRepository, IMapper mapper, AttendeeBusinessRules attendeeBusinessRules)
        {
            _attendeeRepository = attendeeRepository;
            _mapper = mapper;
            _attendeeBusinessRules = attendeeBusinessRules;
        }

        /// <summary>
        /// Handles the query by retrieving a paginated list of all attendees with the specified ticket ID.
        /// </summary>
        /// <param name="request">The query request containing pagination information.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A <see cref="GetListResponse{T}"/> containing a paginated list of attendee DTOs with the specified ticket ID.</returns>
        /// <exception cref="BusinessException">Thrown when no attendee is found with the specified ticket ID.</exception>
        public async Task<GetListResponse<GetListByTicketIdAttendeeListItemDto>> Handle(GetListByTicketIdAttendeeQuery request, CancellationToken cancellationToken)
        {
            await RuleRunner.RunAsync(
                async () => await _attendeeBusinessRules.EnsureTicketExists(request.TicketId)
            );

            Paginate<Attendee> attendees = await _attendeeRepository.GetListAsync(
                predicate: a => a.TicketId == request.TicketId,
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListByTicketIdAttendeeListItemDto> response = _mapper.Map<GetListResponse<GetListByTicketIdAttendeeListItemDto>>(attendees);

            return response;
        }
    }
}