using AutoMapper;
using Core.Application.Rules;
using EventManagement.Application.Features.Reports.Rules;
using EventManagement.Application.Services.Repositories;
using EventManagement.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Application.Features.Reports.Queries.GetById;

/// <summary>
/// Represents a query request for retrieving a specific report by its unique identifier.
/// </summary>
public class GetByIdReportQuery : IRequest<GetByIdReportResponse>
{
    /// <summary>
    /// Gets or sets the unique identifier of the report.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Handles the <see cref="GetByIdReportQuery"/> to return the report details by its unique identifier.
    /// </summary>
    public class GetByIdReportQueryHandler : IRequestHandler<GetByIdReportQuery, GetByIdReportResponse>
    {
        private readonly IReportRepository _reportRepository;
        private readonly IMapper _mapper;
        private readonly ReportBusinessRules _reportBusinessRules;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetByIdReportQueryHandler"/> class.
        /// </summary>
        /// <param name="reportRepository">The repository used to access report data.</param>
        /// <param name="mapper">The AutoMapper instance used for mapping domain entities to DTOs.</param>
        /// <param name="reportBusinessRules">The business rules for validating report-specific constraints.</param>
        public GetByIdReportQueryHandler(IReportRepository reportRepository, IMapper mapper, ReportBusinessRules reportBusinessRules)
        {
            _reportRepository = reportRepository;
            _mapper = mapper;
            _reportBusinessRules = reportBusinessRules;
        }

        /// <summary>
        /// Handles the query to retrieve a report by its unique identifier.
        /// </summary>
        /// <param name="request">The query request containing the report's identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A <see cref="GetByIdReportResponse"/> containing the details of the report.</returns>
        /// <exception cref="NotFoundException">Thrown when no report is found with the specified report ID.</exception>
        public async Task<GetByIdReportResponse> Handle(GetByIdReportQuery request, CancellationToken cancellationToken)
        {
            await RuleRunner.RunAsync(
                async () => await _reportBusinessRules.CheckReportExistsByIdAsync(request.Id)
            );

            Report? reportbyid = await _reportRepository.GetAsync(
                predicate: r => r.Id == request.Id,
                cancellationToken: cancellationToken
            );

            GetByIdReportResponse response = _mapper.Map<GetByIdReportResponse>(reportbyid);

            return response;
        }
    }
}