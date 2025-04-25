using AutoMapper;
using EventManagement.Application.Features.Reports.Commands.Create;
using EventManagement.Application.Features.Reports.Commands.Delete;
using EventManagement.Application.Features.Reports.Commands.Update;
using EventManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}