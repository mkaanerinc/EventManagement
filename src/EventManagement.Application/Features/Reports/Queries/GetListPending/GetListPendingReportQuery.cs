using AutoMapper;
using Core.Application.Models.Requests;
using Core.Application.Models.Responses;
using Core.Infrastructure.Persistence.Paging;
using EventManagement.Application.Services.Repositories;
using EventManagement.Domain.Entities;
using EventManagement.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Application.Features.Reports.Queries.GetListPending;

/// <summary>
/// Represents a query request for retrieving a paginated list of all reports with the specified pending report status.
/// </summary>
public class GetListPendingReportQuery : IRequest<GetListResponse<GetListPendingReportListItemDto>>
{
    /// <summary>
    /// Gets or sets the pagination parameters for the request.
    /// </summary>
    public required PageRequest PageRequest { get; set; }

    /// <summary>
    /// Handles the <see cref="GetListPendingReportQuery"/> to return a paginated list of all reports with the specified pending report status.
    /// </summary>
    public class GetListPendingReportQueryHandler : IRequestHandler<GetListPendingReportQuery, GetListResponse<GetListPendingReportListItemDto>>
    {
        private readonly IReportRepository _reportRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetListPendingReportQueryHandler"/> class.
        /// </summary>
        /// <param name="reportRepository">The repository used to access report data.</param>
        /// <param name="mapper">The AutoMapper instance used for mapping domain entities to DTOs.</param>
        public GetListPendingReportQueryHandler(IReportRepository reportRepository, IMapper mapper)
        {
            _reportRepository = reportRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Handles the query by retrieving a paginated list of all reports with the specified pending report status.
        /// </summary>
        /// <param name="request">The query request containing pagination information.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A <see cref="GetListResponse{T}"/> containing a paginated list of report DTOs.</returns>
        public async Task<GetListResponse<GetListPendingReportListItemDto>> Handle(GetListPendingReportQuery request, CancellationToken cancellationToken)
        {
            Paginate<Report> reports = await _reportRepository.GetListAsync(
                predicate: r => r.ReportStatus == ReportStatus.Pending,
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListPendingReportListItemDto> response = _mapper.Map<GetListResponse<GetListPendingReportListItemDto>>(reports);

            return response;
        }
    }
}