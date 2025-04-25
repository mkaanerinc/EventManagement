using AutoMapper;
using Core.Application.Rules;
using EventManagement.Application.Features.Reports.Rules;
using EventManagement.Application.Services.Repositories;
using EventManagement.Domain.Entities;
using EventManagement.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Application.Features.Reports.Commands.Update;

/// <summary>
/// Command to update a report.
/// </summary>
public class UpdateReportCommand : IRequest<UpdatedReportResponse>
{
    /// <summary>
    /// Gets or sets the unique identifier of the created report.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the related event.
    /// </summary>
    public Guid EventId { get; set; }

    /// <summary>
    /// Gets or sets the type of the report.
    /// </summary>
    public ReportType ReportType { get; set; }

    /// <summary>
    /// Gets or sets the result of the report generation process.
    /// </summary>
    public required string Result { get; set; }

    /// <summary>
    /// Gets or sets the current status of the report.
    /// </summary>
    public ReportStatus ReportStatus { get; set; }

    /// <summary>
    /// Gets or sets the completion timestamp of the report.
    /// </summary>
    public DateTimeOffset? CompletedAt { get; set; }

    /// <summary>
    /// Handles the <see cref="UpdateReportCommand"/> to update a report and return the result.
    /// </summary>
    public class UpdateReportCommandHandler : IRequestHandler<UpdateReportCommand, UpdatedReportResponse>
    {
        private readonly IReportRepository _reportRepository;
        private readonly IMapper _mapper;
        private readonly ReportBusinessRules _reportBusinessRules;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateReportCommandHandler"/> class.
        /// </summary>
        /// <param name="reportRepository">The repository to manage report entities.</param>
        /// <param name="mapper">The mapper instance to convert between models.</param>
        /// <param name="reportBusinessRules">The business rules for validating report-specific constraints.</param>
        public UpdateReportCommandHandler(IReportRepository reportRepository, IMapper mapper, ReportBusinessRules reportBusinessRules)
        {
            _reportRepository = reportRepository;
            _mapper = mapper;
            _reportBusinessRules = reportBusinessRules;
        }

        /// <summary>
        /// Handles the update of a report.
        /// </summary>
        /// <param name="request">The update report command containing report details.</param>
        /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
        /// <returns>The response object containing details of the updated report.</returns>
        /// <exception cref="NotFoundException">Thrown when no report is found with the specified report ID.</exception>
        /// <exception cref="BusinessException">Thrown when no event is found with the specified event ID.</exception>
        public async Task<UpdatedReportResponse> Handle(UpdateReportCommand request, CancellationToken cancellationToken)
        {
            await RuleRunner.RunAsync(
                async () => await _reportBusinessRules.CheckReportExistsByIdAsync(request.Id),
                async () => await _reportBusinessRules.EnsureEventExists(request.EventId)
            );

            Report? updatedReport = await _reportRepository.GetAsync
                (predicate: r => r.Id == request.Id,
                cancellationToken: cancellationToken
                );

            updatedReport = _mapper.Map(request, updatedReport);

            await _reportRepository.UpdateAsync(updatedReport!);

            UpdatedReportResponse response = _mapper.Map<UpdatedReportResponse>(updatedReport);

            return response;
        }
    }
}