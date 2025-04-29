using Core.Infrastructure.Persistence.Repositories;
using EventManagement.Domain.Entities;
using EventManagement.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Application.Services.Repositories;

/// <summary>
/// Represents the contract for the Report repository.
/// Provides asynchronous and synchronous data access methods specific to the <see cref="Report"/> entity.
/// </summary>
public interface IReportRepository : IAsyncRepository<Report,Guid>, IRepository<Report, Guid>
{
    /// <summary>
    /// Retrieves a summary of TicketSales reports for a specific event from the database asynchronously.
    /// </summary>
    /// <param name="eventId">The ID of the event to summarize reports for.</param>
    /// <param name="include">A function to include related entities in the query (e.g., Events).</param>
    /// <param name="withDeleted">Whether to include soft-deleted tickets in the query.</param>
    /// <param name="enableTracking">Whether to enable change tracking for the query.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>A task representing the asynchronous operation, with a summary containing total revenue and total tickets sold.</returns>
    Task<ReportSummary> GetSummaryByEventIdAsync(
        Guid eventId,
        Func<IQueryable<Ticket>, IIncludableQueryable<Ticket, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default);
}