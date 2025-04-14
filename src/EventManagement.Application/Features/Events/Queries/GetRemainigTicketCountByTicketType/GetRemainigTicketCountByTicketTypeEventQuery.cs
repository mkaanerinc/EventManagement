using EventManagement.Application.Features.Events.Queries.GetRemainingTicketCount;
using EventManagement.Application.Services.Repositories;
using EventManagement.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Application.Features.Events.Queries.GetRemainigTicketCountByTicketType;

/// <summary>
/// Represents a query request to get the remaining ticket count for a specific event,
/// optionally filtered by ticket type.
/// </summary>
public class GetRemainigTicketCountByTicketTypeEventQuery : IRequest<GetRemainigTicketCountByTicketTypeEventResponse>
{
    /// <summary>
    /// Gets or sets the unique identifier of the event.
    /// </summary>
    public Guid EventId { get; set; }

    /// <summary>
    /// Gets or sets the optional ticket type to filter.
    /// </summary>
    public TicketType TicketType { get; set; }

    /// <summary>
    /// Handles the <see cref="GetRemainigTicketCountByTicketTypeEventQuery"/> request.
    /// </summary>
    public class GetRemainigTicketCountByTicketTypeEventQueryHandler : IRequestHandler<GetRemainigTicketCountByTicketTypeEventQuery, GetRemainigTicketCountByTicketTypeEventResponse>
    {
        private readonly IEventRepository _eventRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetRemainigTicketCountByTicketTypeEventQueryHandler"/> class.
        /// </summary>
        /// <param name="eventRepository">The repository to access event and ticket data.</param>
        public GetRemainigTicketCountByTicketTypeEventQueryHandler(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        /// <summary>
        /// Handles the query to retrieve the remaining ticket count for an event filtered by ticket type.
        /// </summary>
        /// <param name="request">The query request containing the event ID.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A <see cref="GetRemainigTicketCountByTicketTypeEventResponse"/> with the result.</returns>
        public async Task<GetRemainigTicketCountByTicketTypeEventResponse> Handle(GetRemainigTicketCountByTicketTypeEventQuery request, CancellationToken cancellationToken)
        {
            int remainingCount = await _eventRepository.GetRemainingTicketCountAsync(
            eventPredicate: e => e.Id == request.EventId,
            ticketPredicate: t => t.TicketType == request.TicketType,
            cancellationToken: cancellationToken
            );

            GetRemainigTicketCountByTicketTypeEventResponse response = new()
            {
                RemainingTicketCount = remainingCount
            };

            return response;
        }
    }
}
