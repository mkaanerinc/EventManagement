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

        /// <summary>
        /// Initializes a new instance of the <see cref="GetRemainingTicketCountEventQueryHandler"/> class.
        /// </summary>
        /// <param name="eventRepository">The repository to access event and ticket data.</param>
        public GetRemainingTicketCountEventQueryHandler(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        /// <summary>
        /// Handles the query to retrieve the remaining ticket count for an event.
        /// </summary>
        /// <param name="request">The query request containing the event ID.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A <see cref="GetRemainingTicketCountEventResponse"/> with the result.</returns>
        public async Task<GetRemainingTicketCountEventResponse> Handle(GetRemainingTicketCountEventQuery request, CancellationToken cancellationToken)
        {
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
