using EventManagement.Application.Features.Events.Rules;
using EventManagement.Application.Services.Repositories;
using EventManagement.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Application.Features.Events.Queries.GetRemainingTicketCount;

/// <summary>
/// Represents a query request to get the remaining ticket count for a specific event.
/// </summary>
public class GetRemainingTicketCountEventQuery : IRequest<GetRemainingTicketCountEventResponse>
{
    /// <summary>
    /// Gets or sets the unique identifier of the event.
    /// </summary>
    public Guid EventId { get; set; }

    /// <summary>
    /// Handles the <see cref="GetRemainingTicketCountEventQuery"/> request.
    /// </summary>
    public class GetRemainingTicketCountEventQueryHandler : IRequestHandler<GetRemainingTicketCountEventQuery, GetRemainingTicketCountEventResponse>
    {
        private readonly IEventRepository _eventRepository;
        private readonly EventBusinessRules _eventBusinessRules;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetRemainingTicketCountEventQueryHandler"/> class.
        /// </summary>
        /// <param name="eventRepository">The repository to access event and ticket data.</param>
        /// <param name="eventBusinessRules">The business rules for validating event-specific constraints.</param>
        public GetRemainingTicketCountEventQueryHandler(IEventRepository eventRepository, EventBusinessRules eventBusinessRules)
        {
            _eventRepository = eventRepository;
            _eventBusinessRules = eventBusinessRules;
        }

        /// <summary>
        /// Handles the query to retrieve the remaining ticket count for an event.
        /// </summary>
        /// <param name="request">The query request containing the event ID.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A <see cref="GetRemainingTicketCountEventResponse"/> with the result.</returns>
        /// <exception cref="NotFoundException">Thrown when no event is found with the specified ID.</exception>
        public async Task<GetRemainingTicketCountEventResponse> Handle(GetRemainingTicketCountEventQuery request, CancellationToken cancellationToken)
        {
            await _eventBusinessRules.CheckEventExistsByIdAsync(request.EventId);

            int remainingCount = await _eventRepository.GetRemainingTicketCountAsync(
                eventPredicate: e => e.Id == request.EventId,
                cancellationToken: cancellationToken
            );

            GetRemainingTicketCountEventResponse response = new()
            {
                RemainingTicketCount = remainingCount
            };

            return response;
        }
    }
}
