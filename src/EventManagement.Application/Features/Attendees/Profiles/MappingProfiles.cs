using AutoMapper;
using Core.Application.Models.Responses;
using Core.Infrastructure.Persistence.Paging;
using EventManagement.Application.Features.Attendees.Commands.Create;
using EventManagement.Application.Features.Attendees.Commands.Delete;
using EventManagement.Application.Features.Attendees.Commands.Update;
using EventManagement.Application.Features.Attendees.Queries.GetById;
using EventManagement.Application.Features.Attendees.Queries.GetList;
using EventManagement.Application.Features.Attendees.Queries.GetListByEventId;
using EventManagement.Application.Features.Attendees.Queries.GetListByTicketId;
using EventManagement.Application.Features.Tickets.Queries.GetById;
using EventManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Application.Features.Attendees.Profiles;

/// <summary>
/// Defines AutoMapper mapping configurations for the Attendee entity and related DTOs.
/// </summary>
public class MappingProfiles : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MappingProfiles"/> class and sets up the mapping definitions.
    /// </summary>
    public MappingProfiles()
    {
        /// <summary>
        /// Maps between <see cref="Attendee"/> and <see cref="CreateAttendeeCommand"/> in both directions.
        /// </summary>
        CreateMap<Attendee, CreateAttendeeCommand>().ReverseMap();

        /// <summary>
        /// Maps between <see cref="Attendee"/> and <see cref="CreatedAttendeeResponse"/> in both directions.
        /// </summary>
        CreateMap<Attendee, CreatedAttendeeResponse>().ReverseMap();

        /// <summary>
        /// Maps between <see cref="Attendee"/> and <see cref="DeleteAttendeeCommand"/> in both directions.
        /// </summary>
        CreateMap<Attendee, DeleteAttendeeCommand>().ReverseMap();

        /// <summary>
        /// Maps between <see cref="Attendee"/> and <see cref="DeletedAttendeeResponse"/> in both directions.
        /// </summary>
        CreateMap<Attendee, DeletedAttendeeResponse>().ReverseMap();

        /// <summary>
        /// Maps between <see cref="Attendee"/> and <see cref="UpdateAttendeeCommand"/> in both directions.
        /// </summary>
        CreateMap<Attendee, UpdateAttendeeCommand>().ReverseMap();

        /// <summary>
        /// Maps between <see cref="Attendee"/> and <see cref="UpdatedAttendeeResponse"/> in both directions.
        /// </summary>
        CreateMap<Attendee, UpdatedAttendeeResponse>().ReverseMap();

        /// <summary>
        /// Maps between <see cref="Attendee"/> and <see cref="GetListAttendeeListItemDto"/> in both directions.
        /// </summary>
        CreateMap<Attendee, GetListAttendeeListItemDto>().ReverseMap();

        /// <summary>
        /// Maps between <see cref="Attendee"/> and <see cref="GetListByTicketIdAttendeeListItemDto"/> in both directions.
        /// </summary>
        CreateMap<Attendee, GetListByTicketIdAttendeeListItemDto>().ReverseMap();

        /// <summary>
        /// Maps between <see cref="Attendee"/> and <see cref="GetListByEventIdAttendeeListItemDto"/> in both directions.
        /// </summary>
        CreateMap<Attendee, GetListByEventIdAttendeeListItemDto>().ReverseMap();

        /// <summary>
        /// Maps between <see cref="Attendee"/> and <see cref="GetByIdAttendeeResponse"/> in both directions.
        /// </summary>
        CreateMap<Attendee, GetByIdAttendeeResponse>().ReverseMap();

        /// <summary>
        /// Maps between <see cref="Attendee"/> and <see cref="GetListResponse<GetListAttendeeListItemDto>>"/> in both directions.
        /// </summary>
        CreateMap<Paginate<Attendee>, GetListResponse<GetListAttendeeListItemDto>>().ReverseMap();

        /// <summary>
        /// Maps between <see cref="Attendee"/> and <see cref="GetListResponse<GetListByTicketIdAttendeeListItemDto>>"/> in both directions.
        /// </summary>
        CreateMap<Paginate<Attendee>, GetListResponse<GetListByTicketIdAttendeeListItemDto>>().ReverseMap();

        /// <summary>
        /// Maps between <see cref="Attendee"/> and <see cref="GetListResponse<GetListByEventIdAttendeeListItemDto>>"/> in both directions.
        /// </summary>
        CreateMap<Paginate<Attendee>, GetListResponse<GetListByEventIdAttendeeListItemDto>>().ReverseMap();
    }
}