using AutoMapper;
using EventManagement.Application.Features.Events.Commands.Create;
using EventManagement.Application.Features.Events.Commands.Delete;
using EventManagement.Application.Features.Events.Commands.Update;
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
    }
}
