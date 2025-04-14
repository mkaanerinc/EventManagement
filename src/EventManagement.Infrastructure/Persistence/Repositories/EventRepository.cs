using Core.Infrastructure.Persistence.Repositories;
using EventManagement.Application.Services.Repositories;
using EventManagement.Domain.Entities;
using EventManagement.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Infrastructure.Persistence.Repositories;

/// <summary>
/// Represents the repository for performing operations on <see cref="Event"/> entities.
/// Inherits from <see cref="EFRepositoryBase{Event, Guid, BaseDbContext}"/> to provide common repository functionality.
/// </summary>
public class EventRepository : EFRepositoryBase<Event, Guid, BaseDbContext>, IEventRepository
{
    private readonly BaseDbContext _baseDbContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="EventRepository"/> class.
    /// </summary>
    /// <param name="baseDbContext">The <see cref="BaseDbContext"/> instance used for data access.</param>
    public EventRepository(BaseDbContext baseDbContext) : base(baseDbContext)
    {
        _baseDbContext = baseDbContext;
    }

    /// <summary>
    /// Gets the remaining ticket count for an event based on the specified predicates and conditions.
    /// </summary>
    /// <param name="eventPredicate">An optional predicate to filter the events.</param>
    /// <param name="ticketPredicate">An optional predicate to filter the tickets.</param>
    /// <param name="withDeleted">A flag indicating whether to include deleted entities in the query. Default is <c>false</c>.</param>
    /// <param name="enableTracking">A flag indicating whether to enable entity tracking for change detection. Default is <c>true</c>.</param>
    /// <param name="cancellationToken">A cancellation token to observe while awaiting the operation.</param>
    /// <returns>The remaining ticket count for the filtered events.</returns>
    public async Task<int> GetRemainingTicketCountAsync(Expression<Func<Event, bool>>? eventPredicate = null, Expression<Func<Ticket, bool>>? ticketPredicate = null, bool withDeleted = false, bool enableTracking = true, CancellationToken cancellationToken = default)
    {
        IQueryable<Event> eventQuery = Query();

        if (!enableTracking)
            eventQuery = eventQuery.AsNoTracking();
        if (withDeleted)
            eventQuery = eventQuery.IgnoreQueryFilters();
        if (eventPredicate != null)
            eventQuery = eventQuery.Where(eventPredicate);

        var eventIds = await eventQuery.Select(e => e.Id).ToListAsync(cancellationToken);

        if (!eventIds.Any()) return 0;

        IQueryable<Ticket> ticketQuery = _baseDbContext.Tickets.AsQueryable();

        if (!enableTracking)
            ticketQuery = ticketQuery.AsNoTracking();
        if (withDeleted)
            ticketQuery = ticketQuery.IgnoreQueryFilters();
        if (ticketPredicate != null)
            ticketQuery = ticketQuery.Where(ticketPredicate);

        ticketQuery = ticketQuery.Where(t => eventIds.Contains(t.EventId));

        var totalQuantitySold = await ticketQuery.SumAsync(t => t.QuantitySold, cancellationToken);

        var totalCapacity = await eventQuery.SumAsync(e => e.TotalCapacity, cancellationToken);

        var remainingTickets = totalCapacity - totalQuantitySold;

        return remainingTickets;
    }
}