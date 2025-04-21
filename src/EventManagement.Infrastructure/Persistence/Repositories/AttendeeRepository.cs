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
/// Represents the repository for performing operations on <see cref="Attendee"/> entities.
/// Inherits from <see cref="EFRepositoryBase{Attendee, Guid, BaseDbContext}"/> to provide common repository functionality.
/// </summary>
public class AttendeeRepository : EFRepositoryBase<Attendee,Guid,BaseDbContext>, IAttendeeRepository
{

    /// <summary>
    /// Initializes a new instance of the <see cref="AttendeeRepository"/> class.
    /// </summary>
    /// <param name="baseDbContext">The <see cref="BaseDbContext"/> instance used for data access.</param>
    public AttendeeRepository(BaseDbContext baseDbContext) : base(baseDbContext)
    {
    }

    /// <summary>
    /// Retrieves the total count of attendees for a specific event asynchronously.
    /// </summary>
    /// <param name="eventId">The unique identifier of the event to count attendees for.</param>
    /// <param name="attendeePredicate">An optional predicate to filter attendees. If null, no additional attendee filtering is applied.</param>
    /// <param name="withDeleted">Whether to include soft-deleted attendees in the count. Defaults to false.</param>
    /// <param name="enableTracking">Whether to enable change tracking for the query. Defaults to true.</param>
    /// <param name="cancellationToken">A token to cancel the operation. Defaults to a default cancellation token.</param>
    /// <returns>A task representing the asynchronous operation, with the total count of attendees as the result.</returns>
    public async Task<int> GetCountByEventAsync(Guid eventId, Expression<Func<Attendee, bool>>? attendeePredicate = null,
        bool withDeleted = false, bool enableTracking = true, CancellationToken cancellationToken = default)
    {
        IQueryable<Attendee> attendeeQuery = Query();

        if (!enableTracking)
            attendeeQuery = attendeeQuery.AsNoTracking();
        if (withDeleted)
            attendeeQuery = attendeeQuery.IgnoreQueryFilters();
        if (attendeePredicate != null)
            attendeeQuery = attendeeQuery.Where(attendeePredicate);

        attendeeQuery = attendeeQuery.Include(a => a.Ticket)
                     .Where(a => a.Ticket.EventId == eventId);

        int totalCountOfAttendees = await attendeeQuery.CountAsync(cancellationToken);

        return totalCountOfAttendees;
    }
}