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
/// Represents the contract for the Attendee repository.
/// Provides asynchronous and synchronous data access methods specific to the <see cref="Attendee"/> entity.
/// </summary>
public interface IAttendeeRepository : IAsyncRepository<Attendee, Guid>, IRepository<Attendee, Guid>
{
    /// <summary>
    /// Retrieves the total count of attendees for a specific event asynchronously.
    /// </summary>
    /// <param name="eventId">The ID of the event to count attendees for.</param>
    /// <param name="attendeePredicate">An optional predicate to filter attendees.</param>
    /// <param name="withDeleted">Whether to include soft-deleted attendees in the count.</param>
    /// <param name="enableTracking">Whether to enable change tracking for the query.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>A task representing the asynchronous operation, with the total count of attendees as the result.</returns>
    Task<int> GetCountByEventAsync(
        Guid eventId,
        Expression<Func<Attendee, bool>>? attendeePredicate = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default);
}