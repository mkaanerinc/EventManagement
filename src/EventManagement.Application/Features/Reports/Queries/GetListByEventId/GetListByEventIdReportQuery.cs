using AutoMapper;
using Core.Application.Models.Requests;
using Core.Application.Models.Responses;
using Core.Application.Rules;
using Core.Infrastructure.Persistence.Paging;
using EventManagement.Application.Features.Reports.Rules;
using EventManagement.Application.Services.Repositories;
using EventManagement.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Application.Features.Reports.Queries.GetListByEventId;

/// <summary>
/// Represents a query request for retrieving a paginated list of all reports with the specified event ID.
/// </summary>
public class GetListByEventIdReportQuery : IRequest<GetListResponse<GetListByEventIdReportListItemDto>>
{
    /// <summary>
    /// Gets or sets the pagination parameters for the request.
    /// </summary>
    public required PageRequest PageRequest { get; set; }

    /// <summary>
    /// Gets or sets the ID of the event associated with the report.
    /// </summary>
    public Guid EventId { get; set; }

    /// <summary>
    /// Handles the <see cref="GetListByEventIdReportQuery"/> to return a paginated list of all reports with the specified event ID.
    /// </summary>
    public class GetListByEventIdReportQueryHandler : IRequestHandler<GetListByEventIdReportQuery, GetListResponse<GetListByEventIdReportListItemDto>>
    {
        private readonly IReportRepository _reportRepository;
        private readonly IMapper _mapper;
        private readonly ReportBusinessRules _reportBusinessRules;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetListByEventIdReportQueryHandler"/> class.
        /// </summary>
        /// <param name="reportRepository">The repository used to access report data.</param>
        /// <param name="mapper">The AutoMapper instance used for mapping domain entities to DTOs.</param>
        /// <param name="reportBusinessRules">The business rules for validating report-specific constraints.</param>
        public GetListByEventIdReportQueryHandler(IReportRepository reportRepository, IMapper mapper, ReportBusinessRules reportBusinessRules)
        {
            _reportRepository = reportRepository;
            _mapper = mapper;
            _reportBusinessRules = reportBusinessRules;
        }

        /// <summary>
        /// Handles the query by retrieving a paginated list of all reports with the specified event ID.
        /// </summary>
        /// <param name="request">The query request containing pagination information.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A <see cref="GetListResponse{T}"/> containing a paginated list of report DTOs with the specified event ID.</returns>
        /// <exception cref="BusinessException">Thrown when no report is found with the specified event ID.</exception>
        public async Task<GetListResponse<GetListByEventIdReportListItemDto>> Handle(GetListByEventIdReportQuery request, CancellationToken cancellationToken)
        {
            await RuleRunner.RunAsync(
                async () => await _reportBusinessRules.EnsureEventExists(request.EventId)
            );

            Paginate<Report> reports = await _reportRepository.GetListAsync(
                predicate: r => r.EventId == request.EventId,
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListByEventIdReportListItemDto> response = _mapper.Map<GetListResponse<GetListByEventIdReportListItemDto>>(reports);

            return response;
        }
    }
}