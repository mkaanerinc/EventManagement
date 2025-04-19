using AutoMapper;
using EventManagement.Application.Features.Tickets.Commands.Create;
using EventManagement.Application.Features.Tickets.Commands.Delete;
using EventManagement.Application.Features.Tickets.Commands.Update;
using EventManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Application.Features.Tickets.Profiles;

/// <summary>
/// Defines AutoMapper mapping configurations for the Ticket entity and related DTOs.
/// </summary>
public class MappingProfiles : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MappingProfiles"/> class and sets up the mapping definitions.
    /// </summary>
    public MappingProfiles()
    {
        /// <summary>
        /// Maps between <see cref="Ticket"/> and <see cref="CreateTicketCommand"/> in both directions.
        /// </summary>
        CreateMap<Ticket, CreateTicketCommand>().ReverseMap();

        /// <summary>
        /// Maps between <see cref="Ticket"/> and <see cref="CreatedTicketResponse"/> in both directions.
        /// </summary>
        CreateMap<Ticket, CreatedTicketResponse>().ReverseMap();

        /// <summary>
        /// Maps between <see cref="Ticket"/> and <see cref="DeleteTicketCommand"/> in both directions.
        /// </summary>
        CreateMap<Ticket, DeleteTicketCommand>().ReverseMap();

        /// <summary>
        /// Maps between <see cref="Ticket"/> and <see cref="DeletedTicketResponse"/> in both directions.
        /// </summary>
        CreateMap<Ticket, DeletedTicketResponse>().ReverseMap();

        /// <summary>
        /// Maps between <see cref="Ticket"/> and <see cref="UpdateTicketCommand"/> in both directions.
        /// </summary>
        CreateMap<Ticket, UpdateTicketCommand>().ReverseMap();

        /// <summary>
        /// Maps between <see cref="Ticket"/> and <see cref="UpdatedTicketResponse"/> in both directions.
        /// </summary>
        CreateMap<Ticket, UpdatedTicketResponse>().ReverseMap();
    }
}