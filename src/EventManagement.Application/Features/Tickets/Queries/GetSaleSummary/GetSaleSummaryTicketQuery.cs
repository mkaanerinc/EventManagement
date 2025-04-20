using Core.Application.Rules;
using Core.Infrastructure.Persistence.Paging;
using EventManagement.Application.Features.Tickets.Rules;
using EventManagement.Application.Services.Repositories;
using EventManagement.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Application.Features.Tickets.Queries.GetSaleSummary;

/// <summary>
/// Represents a query request for retrieving the ticket sales summary for a specific event by its event ID.
/// </summary>
public class GetSaleSummaryTicketQuery : IRequest<GetSaleSummaryTicketResponse>
{
    /// <summary>
    /// Gets or sets the unique identifier of the event.
    /// </summary>
    public Guid EventId { get; set; }

    /// <summary>
    /// Handles the <see cref="GetSaleSummaryTicketQuery"/> to return the ticket sales summary for an event.
    /// </summary>
    public class GetSaleSummaryTicketQueryHandler : IRequestHandler<GetSaleSummaryTicketQuery, GetSaleSummaryTicketResponse>
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly TicketBusinessRules _ticketBusinessRules;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetSaleSummaryTicketQueryHandler"/> class.
        /// </summary>
        /// <param name="ticketRepository">The repository used to access ticket data.</param>
        /// <param name="ticketBusinessRules">The business rules for validating ticket-specific constraints.</param>
        public GetSaleSummaryTicketQueryHandler(ITicketRepository ticketRepository, TicketBusinessRules ticketBusinessRules)
        {
            _ticketRepository = ticketRepository;
            _ticketBusinessRules = ticketBusinessRules;
        }

        /// <summary>
        /// Handles the query to retrieve the ticket sales summary for a specific event.
        /// </summary>
        /// <param name="request">The query request containing the event's identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A <see cref="GetSaleSummaryTicketResponse"/> containing the ticket sales summary.</returns>
        /// <exception cref="NotFoundException">Thrown when no event is found with the specified ID.</exception>
        public async Task<GetSaleSummaryTicketResponse> Handle(GetSaleSummaryTicketQuery request, CancellationToken cancellationToken)
        {
            await RuleRunner.RunAsync(
                async () => await _ticketBusinessRules.CheckEventExistsByIdAsync(request.EventId)
            );

            Paginate<Ticket> tickets = await _ticketRepository.GetListAsync(
                predicate: t => t.EventId == request.EventId,
                cancellationToken: cancellationToken
            );

            GetSaleSummaryTicketResponse response = new GetSaleSummaryTicketResponse
            {
                EventId = request.EventId,
                TotalRevenue = tickets.Items.Sum(t => t.Price * t.QuantitySold),
                TotalTicketsSold = tickets.Items.Sum(t => t.QuantitySold),
                TicketSalesDetails = tickets.Items.Select(t => new TicketSalesDetail
                {
                    Id = t.Id,
                    TicketType = t.TicketType,
                    Price = t.Price,
                    QuantitySold = t.QuantitySold,
                    Revenue = t.Price * t.QuantitySold
                }).ToList()
            };

            return response;
        }
    }
}