using AutoMapper;
using Core.Application.Rules;
using EventManagement.Application.Features.Attendees.Rules;
using EventManagement.Application.Services.Repositories;
using EventManagement.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Application.Features.Attendees.Queries.GetById;

/// <summary>
/// Represents a query request for retrieving a specific attendee by its unique identifier.
/// </summary>
public class GetByIdAttendeeQuery : IRequest<GetByIdAttendeeResponse>
{
    /// <summary>
    /// Gets or sets the unique identifier of the attendee.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Handles the <see cref="GetByIdAttendeeQuery"/> to return the attendee details by its unique identifier.
    /// </summary>
    public class GetByIdAttendeeQueryHandler : IRequestHandler<GetByIdAttendeeQuery, GetByIdAttendeeResponse>
    {
        private readonly IAttendeeRepository _attendeeRepository;
        private readonly IMapper _mapper;
        private readonly AttendeeBusinessRules _attendeeBusinessRules;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetByIdAttendeeQueryHandler"/> class.
        /// </summary>
        /// <param name="attendeeRepository">The repository used to access attendee data.</param>
        /// <param name="mapper">The AutoMapper instance used for mapping domain entities to DTOs.</param>
        /// <param name="attendeeBusinessRules">The business rules for validating attendee-specific constraints.</param>
        public GetByIdAttendeeQueryHandler(IAttendeeRepository attendeeRepository, IMapper mapper, AttendeeBusinessRules attendeeBusinessRules)
        {
            _attendeeRepository = attendeeRepository;
            _mapper = mapper;
            _attendeeBusinessRules = attendeeBusinessRules;
        }

        /// <summary>
        /// Handles the query to retrieve an attendee by its unique identifier.
        /// </summary>
        /// <param name="request">The query request containing the attendee's identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A <see cref="GetByIdAttendeeResponse"/> containing the details of the attendee.</returns>
        /// <exception cref="NotFoundException">Thrown when no attendee is found with the specified ID.</exception>
        public async Task<GetByIdAttendeeResponse> Handle(GetByIdAttendeeQuery request, CancellationToken cancellationToken)
        {
            await RuleRunner.RunAsync(
                async () => await _attendeeBusinessRules.CheckAttendeeExistsByIdAsync(request.Id)
            );

            Attendee? attendeebyid = await _attendeeRepository.GetAsync(
                predicate: a => a.Id == request.Id,
                cancellationToken: cancellationToken
            );

            GetByIdAttendeeResponse response =  _mapper.Map<GetByIdAttendeeResponse>(attendeebyid);

            return response;
        }
    }
}