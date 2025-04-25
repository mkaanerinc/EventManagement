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

namespace EventManagement.Application.Features.Reports.Commands.Create;

/// <summary>
/// Command to create a new report.
/// </summary>
public class CreateReportCommand : IRequest<CreatedReportResponse>
{
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
    /// Handles the <see cref="CreateReportCommand"/> to create a new report and return the result.
    /// </summary>
    public class CreateReportCommandHandler : IRequestHandler<CreateReportCommand, CreatedReportResponse>
    {
        private readonly IReportRepository _reportRepository;
        private readonly IMapper _mapper;
        private readonly ReportBusinessRules _reportBusinessRules;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateReportCommandHandler"/> class.
        /// </summary>
        /// <param name="reportRepository">The repository to manage report entities.</param>
        /// <param name="mapper">The mapper instance to convert between models.</param>
        /// <param name="reportBusinessRules">The business rules for validating report-specific constraints.</param>
        public CreateReportCommandHandler(IReportRepository reportRepository, IMapper mapper, ReportBusinessRules reportBusinessRules)
        {
            _reportRepository = reportRepository;
            _mapper = mapper;
            _reportBusinessRules = reportBusinessRules;
        }

        /// <summary>
        /// Handles the creation of a new report.
        /// </summary>
        /// <param name="request">The create report command containing report details.</param>
        /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
        /// <returns>The response object containing details of the created report.</returns>
        /// <exception cref="BusinessException">Thrown when no event is found with the specified event ID.</exception>
        public async Task<CreatedReportResponse> Handle(CreateReportCommand request, CancellationToken cancellationToken)
        {
            await RuleRunner.RunAsync(
                async () => await _reportBusinessRules.EnsureEventExists(request.EventId)
            );

            Report newReport = _mapper.Map<Report>(request);
            newReport.Id = Guid.NewGuid();

            await _reportRepository.AddAsync(newReport);

            CreatedReportResponse response = _mapper.Map<CreatedReportResponse>(newReport);

            return response;
        }
    }
}