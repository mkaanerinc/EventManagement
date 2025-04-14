using AutoMapper;
using Core.Application.Models.Responses;
using Core.Infrastructure.Persistence.Paging;
using EventManagement.Application.Features.Events.Commands.Create;
using EventManagement.Application.Features.Events.Commands.Delete;
using EventManagement.Application.Features.Events.Commands.Update;
using EventManagement.Application.Features.Events.Queries.GetById;
using EventManagement.Application.Features.Events.Queries.GetList;
using EventManagement.Application.Features.Events.Queries.GetListByDate;
using EventManagement.Application.Features.Events.Queries.GetListByDateRange;
using EventManagement.Application.Features.Events.Queries.GetListOfUpcoming;
using EventManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Application.Features.Events.Profiles;

/// <summary>
/// Defines AutoMapper mapping configurations for the Event entity and related DTOs.
/// </summary>
public class MappingProfiles : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MappingProfiles"/> class and sets up the mapping definitions.
    /// </summary>
    public MappingProfiles()
    {
        /// <summary>
        /// Maps between <see cref="Event"/> and <see cref="CreateEventCommand"/> in both directions.
        /// </summary>
        CreateMap<Event, CreateEventCommand>().ReverseMap();

        /// <summary>
        /// Maps between <see cref="Event"/> and <see cref="CreatedEventResponse"/> in both directions.
        /// </summary>
        CreateMap<Event, CreatedEventResponse>().ReverseMap();

        /// <summary>
        /// Maps between <see cref="Event"/> and <see cref="DeleteEventCommand"/> in both directions.
        /// </summary>
        CreateMap<Event, DeleteEventCommand>().ReverseMap();

        /// <summary>
        /// Maps between <see cref="Event"/> and <see cref="DeletedEventResponse"/> in both directions.
        /// </summary>
        CreateMap<Event, DeletedEventResponse>().ReverseMap();

        /// <summary>
        /// Maps between <see cref="Event"/> and <see cref="UpdateEventCommand"/> in both directions.
        /// </summary>
        CreateMap<Event, UpdateEventCommand>().ReverseMap();

        /// <summary>
        /// Maps between <see cref="Event"/> and <see cref="UpdatedEventResponse"/> in both directions.
        /// </summary>
        CreateMap<Event, UpdatedEventResponse>().ReverseMap();

        /// <summary>
        /// Maps between <see cref="Event"/> and <see cref="GetListEventListItemDto"/> in both directions.
        /// </summary>
        CreateMap<Event, GetListEventListItemDto>().ReverseMap();

        /// <summary>
        /// Maps between <see cref="Event"/> and <see cref="GetListByDateEventListItemDto"/> in both directions.
        /// </summary>
        CreateMap<Event, GetListByDateEventListItemDto>().ReverseMap();

        /// <summary>
        /// Maps between <see cref="Event"/> and <see cref="GetListByDateRangeEventListItemDto"/> in both directions.
        /// </summary>
        CreateMap<Event, GetListByDateRangeEventListItemDto>().ReverseMap();

        /// <summary>
        /// Maps between <see cref="Event"/> and <see cref="GetListOfUpcomingEventListItemDto"/> in both directions.
        /// </summary>
        CreateMap<Event, GetListOfUpcomingEventListItemDto>().ReverseMap();

        /// <summary>
        /// Maps between <see cref="Event"/> and <see cref="GetByIdEventResponse"/> in both directions.
        /// </summary>
        CreateMap<Event, GetByIdEventResponse>().ReverseMap();

        /// <summary>
        /// Maps between <see cref="Event"/> and <see cref="GetListResponse<GetListEventListItemDto>>"/> in both directions.
        /// </summary>
        CreateMap<Paginate<Event>, GetListResponse<GetListEventListItemDto>>().ReverseMap();

        /// <summary>
        /// Maps between <see cref="Event"/> and <see cref="GetListResponse<GetListByDateEventListItemDto>>"/> in both directions.
        /// </summary>
        CreateMap<Paginate<Event>, GetListResponse<GetListByDateEventListItemDto>>().ReverseMap();

        /// <summary>
        /// Maps between <see cref="Event"/> and <see cref="GetListResponse<GetListByDateRangeEventListItemDto>>"/> in both directions.
        /// </summary>
        CreateMap<Paginate<Event>, GetListResponse<GetListByDateRangeEventListItemDto>>().ReverseMap();

        /// <summary>
        /// Maps between <see cref="Event"/> and <see cref="GetListResponse<GetListOfUpcomingEventListItemDto>>"/> in both directions.
        /// </summary>
        CreateMap<Paginate<Event>, GetListResponse<GetListOfUpcomingEventListItemDto>>().ReverseMap();
    }
}
