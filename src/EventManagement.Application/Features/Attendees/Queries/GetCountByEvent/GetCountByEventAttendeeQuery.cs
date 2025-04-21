using Core.Application.Rules;
using EventManagement.Application.Features.Attendees.Rules;
using EventManagement.Application.Services.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Application.Features.Attendees.Queries.GetCountByEvent;

/// <summary>
/// Represents a query to retrieve the total count of attendees for a specific event.
/// </summary>
public class GetCountByEventAttendeeQuery : IRequest<GetCountByEventAttendeeResponse>
{
    /// <summary>
    /// Gets or sets the unique identifier of the event to count attendees for.
    /// </summary>
    public Guid EventId { get; set; }

    /// <summary>
    /// Handles the <see cref="GetCountByEventAttendeeQuery"/> to retrieve the total count of attendees for an event.
    /// </summary>
    public class GetCountByEventAttendeeQueryHandler : IRequestHandler<GetCountByEventAttendeeQuery, GetCountByEventAttendeeResponse>
    {
        private readonly IAttendeeRepository _attendeeRepository;
        private readonly AttendeeBusinessRules _attendeeBusinessRules;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetCountByEventAttendeeQueryHandler"/> class.
        /// </summary>
        /// <param name="attendeeRepository">The repository to access attendee and attendee data.</param>
        /// <param name="attendeeBusinessRules">The business rules for validating attendee-specific constraints.</param>
        public GetCountByEventAttendeeQueryHandler(IAttendeeRepository attendeeRepository, AttendeeBusinessRules attendeeBusinessRules)
        {
            _attendeeRepository = attendeeRepository;
            _attendeeBusinessRules = attendeeBusinessRules;
        }

        /// <summary>
        /// Processes the query to retrieve the total count of attendees for the specified event.
        /// </summary>
        /// <param name="request">The query request containing the event ID.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A <see cref="GetCountByEventAttendeeResponse"/> with the result.</returns>
        /// <exception cref="NotFoundException">Thrown when no event is found with the specified ID.</exception>
        public async Task<GetCountByEventAttendeeResponse> Handle(GetCountByEventAttendeeQuery request, CancellationToken cancellationToken)
        {
            await RuleRunner.RunAsync(
                async () => await _attendeeBusinessRules.EnsureEventExists(request.EventId)
            );

            int totalCountOfAttendees = await _attendeeRepository.GetCountByEventAsync(
                request.EventId,
                cancellationToken: cancellationToken
            );

            GetCountByEventAttendeeResponse response = new() { TotalCountOfAttendees = totalCountOfAttendees};

            return response;
        }
    }
}