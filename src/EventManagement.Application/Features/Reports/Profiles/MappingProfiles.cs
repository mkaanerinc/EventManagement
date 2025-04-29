using AutoMapper;
using Core.Application.Models.Responses;
using Core.Infrastructure.Persistence.Paging;
using EventManagement.Application.Features.Reports.Commands.Create;
using EventManagement.Application.Features.Reports.Commands.Delete;
using EventManagement.Application.Features.Reports.Commands.Update;
using EventManagement.Application.Features.Reports.Queries.GetListByEventId;
using EventManagement.Application.Features.Reports.Queries.GetById;
using EventManagement.Application.Features.Reports.Queries.GetList;
using EventManagement.Application.Features.Reports.Queries.GetListCompleted;
using EventManagement.Application.Features.Reports.Queries.GetListFailed;
using EventManagement.Application.Features.Reports.Queries.GetListPending;
using EventManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventManagement.Domain.ValueObjects;
using EventManagement.Application.Features.Reports.Queries.GetSummaryByEventId;

namespace EventManagement.Application.Features.Reports.Profiles;

/// <summary>
/// Defines AutoMapper mapping configurations for the Report entity and related DTOs.
/// </summary>
public class MappingProfiles : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MappingProfiles"/> class and sets up the mapping definitions.
    /// </summary>
    public MappingProfiles()
    {
        /// <summary>
        /// Maps between <see cref="Report"/> and <see cref="CreateReportCommand"/> in both directions.
        /// </summary>
        CreateMap<Report, CreateReportCommand>().ReverseMap();

        /// <summary>
        /// Maps between <see cref="Report"/> and <see cref="CreatedReportResponse"/> in both directions.
        /// </summary>
        CreateMap<Report, CreatedReportResponse>().ReverseMap();

        /// <summary>
        /// Maps between <see cref="Report"/> and <see cref="DeleteReportCommand"/> in both directions.
        /// </summary>
        CreateMap<Report, DeleteReportCommand>().ReverseMap();

        /// <summary>
        /// Maps between <see cref="Report"/> and <see cref="DeletedReportResponse"/> in both directions.
        /// </summary>
        CreateMap<Report, DeletedReportResponse>().ReverseMap();

        /// <summary>
        /// Maps between <see cref="Report"/> and <see cref="UpdateReportCommand"/> in both directions.
        /// </summary>
        CreateMap<Report, UpdateReportCommand>().ReverseMap();

        /// <summary>
        /// Maps between <see cref="Report"/> and <see cref="UpdatedReportResponse"/> in both directions.
        /// </summary>
        CreateMap<Report, UpdatedReportResponse>().ReverseMap();

        /// <summary>
        /// Maps between <see cref="Report"/> and <see cref="GetListByEventIdReportListItemDto"/> in both directions.
        /// </summary>
        CreateMap<Report, GetListByEventIdReportListItemDto>().ReverseMap();

        /// <summary>
        /// Maps between <see cref="Report"/> and <see cref="GetListReportListItemDto"/> in both directions.
        /// </summary>
        CreateMap<Report, GetListReportListItemDto>().ReverseMap();

        /// <summary>
        /// Maps between <see cref="Report"/> and <see cref="GetListCompletedReportListItemDto"/> in both directions.
        /// </summary>
        CreateMap<Report, GetListCompletedReportListItemDto>().ReverseMap();

        /// <summary>
        /// Maps between <see cref="Report"/> and <see cref="GetListFailedReportListItemDto"/> in both directions.
        /// </summary>
        CreateMap<Report, GetListFailedReportListItemDto>().ReverseMap();

        /// <summary>
        /// Maps between <see cref="Report"/> and <see cref="GetListPendingReportListItemDto"/> in both directions.
        /// </summary>
        CreateMap<Report, GetListPendingReportListItemDto>().ReverseMap();

        /// <summary>
        /// Maps between <see cref="Report"/> and <see cref="GetByIdReportResponse"/> in both directions.
        /// </summary>
        CreateMap<Report, GetByIdReportResponse>().ReverseMap();

        /// <summary>
        /// Maps between <see cref="ReportSummary"/> and <see cref="GetSummaryByEventIdReportResponse"/> in both directions.
        /// </summary>
        CreateMap<ReportSummary, GetSummaryByEventIdReportResponse>().ReverseMap();

        /// <summary>
        /// Maps between <see cref="Report"/> and <see cref="GetListResponse<GetListByEventIdReportListItemDto>>"/> in both directions.
        /// </summary>
        CreateMap<Paginate<Report>, GetListResponse<GetListByEventIdReportListItemDto>>().ReverseMap();

        /// <summary>
        /// Maps between <see cref="Report"/> and <see cref="GetListResponse<GetListReportListItemDto>>"/> in both directions.
        /// </summary>
        CreateMap<Paginate<Report>, GetListResponse<GetListReportListItemDto>>().ReverseMap();

        /// <summary>
        /// Maps between <see cref="Report"/> and <see cref="GetListResponse<GetListCompletedReportListItemDto>>"/> in both directions.
        /// </summary>
        CreateMap<Paginate<Report>, GetListResponse<GetListCompletedReportListItemDto>>().ReverseMap();

        /// <summary>
        /// Maps between <see cref="Report"/> and <see cref="GetListResponse<GetListFailedReportListItemDto>>"/> in both directions.
        /// </summary>
        CreateMap<Paginate<Report>, GetListResponse<GetListFailedReportListItemDto>>().ReverseMap();

        /// <summary>
        /// Maps between <see cref="Report"/> and <see cref="GetListResponse<GetListPendingReportListItemDto>>"/> in both directions.
        /// </summary>
        CreateMap<Paginate<Report>, GetListResponse<GetListPendingReportListItemDto>>().ReverseMap();
    }
}