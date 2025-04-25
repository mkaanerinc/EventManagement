using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using EventManagement.Application.Features.Reports.Constants;
using EventManagement.Application.Services.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Application.Features.Reports.Rules;

/// <summary>
/// Contains business rules related to the report management functionality.
/// </summary>
public class ReportBusinessRules : BaseBusinessRules
{
    private readonly IReportRepository _reportRepository;
    private readonly IEventRepository _eventRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="ReportBusinessRules"/> class.
    /// </summary>
    /// <param name="reportRepository">The report repository used to access report data.</param>
    /// <param name="eventRepository">The event repository used to access event data.</param>
    public ReportBusinessRules(IReportRepository reportRepository, IEventRepository eventRepository)
    {
        _reportRepository = reportRepository;
        _eventRepository = eventRepository;
    }

    /// <summary>
    /// Checks if a report with the specified ID exists in the repository.
    /// Throws a NotFoundException if the report is not found.
    /// </summary>
    /// <param name="reportId">The ID of the report to check.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    /// <exception cref="NotFoundException">Thrown when the report with the given ID is not found.</exception>
    public async Task CheckReportExistsByIdAsync(Guid reportId)
    {
        bool reportExists = await _reportRepository.AnyAsync(r => r.Id == reportId);

        if (!reportExists)
        {
            throw new NotFoundException(string.Format(ReportsMessages.NotFoundById, reportId));
        }
    }

    /// <summary>
    /// Ensures that the specified event exists in the system.
    /// </summary>
    /// <param name="eventId">The unique identifier of the event.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    /// <exception cref="BusinessException">
    /// Thrown when no event with the given <paramref name="eventId"/> is found.
    /// </exception>
    public async Task EnsureEventExists(Guid eventId)
    {
        var exists = await _eventRepository.AnyAsync(e => e.Id == eventId);
        if (!exists)
            throw new BusinessException(ReportsMessages.NotFoundEvent);
    }
}