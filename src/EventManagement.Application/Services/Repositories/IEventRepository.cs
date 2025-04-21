using Core.Infrastructure.Persistence.Repositories;
using EventManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Application.Services.Repositories;

/// <summary>
/// Represents the contract for the Event repository.
/// Provides asynchronous and synchronous data access methods specific to the <see cref="Event"/> entity.
/// </summary>
public interface IEventRepository : IAsyncRepository<Event, Guid>, IRepository<Event, Guid>
{
    /// <summary>
    /// Retrieves the total remaining ticket count for events matching the specified criteria asynchronously.
    /// </summary>
    /// <param name="eventPredicate">An optional predicate to filter events. If null, no event filtering is applied.</param>
    /// <param name="ticketPredicate">An optional predicate to filter tickets. If null, no ticket filtering is applied.</param>
    /// <param name="withDeleted">Whether to include soft-deleted events and tickets in the count. Defaults to false.</param>
    /// <param name="enableTracking">Whether to enable change tracking for the query. Defaults to true.</param>
    /// <param name="cancellationToken">A token to cancel the operation. Defaults to a default cancellation token.</param>
    /// <returns>A task representing the asynchronous operation, with the total remaining ticket count as the result.</returns>
    Task<int> GetRemainingTicketCountAsync(
    Expression<Func<Event, bool>>? eventPredicate = null,
    Expression<Func<Ticket, bool>>? ticketPredicate = null,
    bool withDeleted = false,
    bool enableTracking = true,
    CancellationToken cancellationToken = default);
}