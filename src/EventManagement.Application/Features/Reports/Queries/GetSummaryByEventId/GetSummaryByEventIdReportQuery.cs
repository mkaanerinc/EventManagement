using AutoMapper;
using Core.Application.Rules;
using EventManagement.Application.Features.Reports.Rules;
using EventManagement.Application.Services.Repositories;
using EventManagement.Domain.ValueObjects;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Application.Features.Reports.Queries.GetSummaryByEventId;

/// <summary>
/// Represents a query request for retrieving a specific report summary by event ID.
/// </summary>
public class GetSummaryByEventIdReportQuery : IRequest<GetSummaryByEventIdReportResponse>
{
    /// <summary>
    /// Gets or sets the ID of the event associated with the report.
    /// </summary>
    public Guid EventId { get; set; }

    /// <summary>
    /// Handles the <see cref="GetSummaryByEventIdReportQuery"/> to return the report summary by event ID.
    /// </summary>
    public class GetSummaryByEventIdReportQueryHandler : IRequestHandler<GetSummaryByEventIdReportQuery, GetSummaryByEventIdReportResponse>
    {
        private readonly IReportRepository _reportRepository;
        private readonly IMapper _mapper;
        private readonly ReportBusinessRules _reportBusinessRules;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetSummaryByEventIdReportQueryHandler"/> class.
        /// </summary>
        /// <param name="reportRepository">The repository used to access report data.</param>
        /// <param name="mapper">The AutoMapper instance used for mapping domain entities to DTOs.</param>
        /// <param name="reportBusinessRules">The business rules for validating report-specific constraints.</param>
        public GetSummaryByEventIdReportQueryHandler(IReportRepository reportRepository, IMapper mapper, ReportBusinessRules reportBusinessRules)
        {
            _reportRepository = reportRepository;
            _mapper = mapper;
            _reportBusinessRules = reportBusinessRules;
        }

        /// <summary>
        /// Handles the query to retrieve a report summary by event ID.
        /// </summary>
        /// <param name="request">The query request containing event ID.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A <see cref="GetSummaryByEventIdReportResponse"/> containing the report summary with the specified event ID.</returns>
        /// <exception cref="BusinessException">Thrown when no report is found with the specified event ID.</exception>
        public async Task<GetSummaryByEventIdReportResponse> Handle(GetSummaryByEventIdReportQuery request, CancellationToken cancellationToken)
        {
            await RuleRunner.RunAsync(
                async () => await _reportBusinessRules.EnsureEventExists(request.EventId)
            );

            ReportSummary reportSummary = await _reportRepository.GetSummaryByEventIdAsync(
                request.EventId,
                cancellationToken: cancellationToken
            );

            GetSummaryByEventIdReportResponse response = _mapper.Map<GetSummaryByEventIdReportResponse>(reportSummary);

            return response;
        }
    }
}