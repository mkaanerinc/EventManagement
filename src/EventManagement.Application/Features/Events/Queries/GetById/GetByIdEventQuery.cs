using AutoMapper;
using Core.Application.Rules;
using EventManagement.Application.Features.Events.Rules;
using EventManagement.Application.Services.Repositories;
using EventManagement.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Application.Features.Events.Queries.GetById;

/// <summary>
/// Represents a query request for retrieving a specific event by its unique identifier.
/// </summary>
public class GetByIdEventQuery : IRequest<GetByIdEventResponse>
{
    /// <summary>
    /// Gets or sets the unique identifier of the event.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Handles the <see cref="GetByIdEventQuery"/> to return the event details by its unique identifier.
    /// </summary>
    public class GetByIdEventQueryHandler : IRequestHandler<GetByIdEventQuery, GetByIdEventResponse>
    {
        private readonly IEventRepository _eventRepository;
        private readonly IMapper _mapper;
        private readonly EventBusinessRules _eventBusinessRules;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetByIdEventQueryHandler"/> class.
        /// </summary>
        /// <param name="eventRepository">The repository used to access event data.</param>
        /// <param name="mapper">The AutoMapper instance used for mapping domain entities to DTOs.</param>
        /// <param name="eventBusinessRules">The business rules for validating event-specific constraints.</param>
        public GetByIdEventQueryHandler(IEventRepository eventRepository, IMapper mapper, EventBusinessRules eventBusinessRules)
        {
            _eventRepository = eventRepository;
            _mapper = mapper;
            _eventBusinessRules = eventBusinessRules;
        }

        /// <summary>
        /// Handles the query to retrieve an event by its unique identifier.
        /// </summary>
        /// <param name="request">The query request containing the event's identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A <see cref="GetByIdEventResponse"/> containing the details of the event.</returns>
        /// <exception cref="NotFoundException">Thrown when no event is found with the specified event ID.</exception>
        public async Task<GetByIdEventResponse> Handle(GetByIdEventQuery request, CancellationToken cancellationToken)
        {
            await RuleRunner.RunAsync(
                async () => await _eventBusinessRules.CheckEventExistsByIdAsync(request.Id)
            );

            Event? eventbyid = await _eventRepository.GetAsync(
                predicate: e => e.Id == request.Id,
                cancellationToken: cancellationToken
            );

            GetByIdEventResponse response = _mapper.Map<GetByIdEventResponse>(eventbyid);

            return response;
        }
    }
}