using Core.Infrastructure.Persistence.Repositories;
using EventManagement.Application.Services.Repositories;
using EventManagement.Domain.Entities;
using EventManagement.Domain.ValueObjects;
using EventManagement.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Infrastructure.Persistence.Repositories;

/// <summary>
/// Represents the repository for performing operations on <see cref="Report"/> entities.
/// Inherits from <see cref="EFRepositoryBase{Report, Guid, BaseDbContext}"/> to provide common repository functionality.
/// </summary>
public class ReportRepository : EFRepositoryBase<Report,Guid,BaseDbContext>, IReportRepository
{
    private readonly BaseDbContext _baseDbContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="ReportRepository"/> class.
    /// </summary>
    /// <param name="baseDbContext">The <see cref="BaseDbContext"/> instance used for data access.</param>
    public ReportRepository(BaseDbContext baseDbContext) : base(baseDbContext)
    {
        _baseDbContext = baseDbContext;
    }

    /// <summary>
    /// Retrieves a summary of TicketSales reports for a specific event from the database asynchronously.
    /// </summary>
    /// <param name="eventId">The ID of the event to summarize reports for.</param>
    /// <param name="include">A function to include related entities in the query (e.g., Events).</param>
    /// <param name="withDeleted">Whether to include soft-deleted tickets in the query.</param>
    /// <param name="enableTracking">Whether to enable change tracking for the query.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>A task representing the asynchronous operation, with a summary containing total revenue and total tickets sold.</returns>
    public async Task<ReportSummary> GetSummaryByEventIdAsync(Guid eventId, Func<IQueryable<Ticket>, IIncludableQueryable<Ticket, object>>? include = null, bool withDeleted = false, bool enableTracking = true, CancellationToken cancellationToken = default)
    {
        IQueryable<Ticket> ticketQuery = _baseDbContext.Tickets.AsQueryable();

        if (!enableTracking)
            ticketQuery = ticketQuery.AsNoTracking();
        if (withDeleted)
            ticketQuery = ticketQuery.IgnoreQueryFilters();
        if (include != null)
            ticketQuery = include(ticketQuery);

        ticketQuery = ticketQuery.Where(t => t.EventId == eventId);

        var reportSummary = await ticketQuery
            .GroupBy(t => t.EventId)
            .Select(g => new ReportSummary
            {
                EventId = g.Key,
                TotalRevenue = g.Sum(t => t.Price * t.QuantitySold),
                TotalTicketsSold = g.Sum(t => t.QuantitySold)
            })
            .FirstOrDefaultAsync(cancellationToken);

        return reportSummary ?? new ReportSummary
        {
            EventId = eventId,
            TotalRevenue = 0,
            TotalTicketsSold = 0
        };
    }
}