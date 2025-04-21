using Core.Application.Models.Requests;
using Core.Application.Models.Responses;
using Core.CrossCuttingConcerns.Exceptions.HttpProblemDetails;
using EventManagement.Application.Features.Attendees.Commands.CheckIn;
using EventManagement.Application.Features.Attendees.Commands.Create;
using EventManagement.Application.Features.Attendees.Commands.Delete;
using EventManagement.Application.Features.Attendees.Commands.Update;
using EventManagement.Application.Features.Attendees.Queries.GetById;
using EventManagement.Application.Features.Attendees.Queries.GetCountByEvent;
using EventManagement.Application.Features.Attendees.Queries.GetList;
using EventManagement.Application.Features.Attendees.Queries.GetListByEventId;
using EventManagement.Application.Features.Attendees.Queries.GetListByTicketId;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ValidationProblemDetails = Core.CrossCuttingConcerns.Exceptions.HttpProblemDetails.ValidationProblemDetails;

namespace EventManagement.Api.Controllers;

/// <summary>
/// API controller for managing attendees.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class AttendeesController : BaseController
{
    /// <summary>
    /// Creates a new attendee.
    /// </summary>
    /// <param name="createAttendeeCommand">The attendee creation command.</param>
    /// <returns>The created attendee details.</returns>
    /// <response code="201">Attendee successfully created.</response>
    /// <response code="400">Invalid input data.</response>
    /// <response code="500">Server error.</response>
    [HttpPost]
    [ProducesResponseType(typeof(CreatedAttendeeResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(InternalServerErrorProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Create([FromBody] CreateAttendeeCommand createAttendeeCommand)
    {
        CreatedAttendeeResponse response = await Mediator!.Send(createAttendeeCommand);

        return Created(string.Empty, response);
    }

    /// <summary>
    /// Updates an existing attendee.
    /// </summary>
    /// <param name="updateAttendeeCommand">The attendee update command.</param>
    /// <returns>The updated attendee details.</returns>
    /// <response code="200">Attendee updated successfully and returned.</response>
    /// <response code="400">Invalid input.</response>
    /// <response code="404">Attendee not found with the specified ID.</response>
    /// <response code="500">Server error.</response>
    [HttpPut]
    [ProducesResponseType(typeof(UpdatedAttendeeResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(NotFoundProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(InternalServerErrorProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update([FromBody] UpdateAttendeeCommand updateAttendeeCommand)
    {
        UpdatedAttendeeResponse response = await Mediator!.Send(updateAttendeeCommand);

        return Ok(response);
    }

    /// <summary>
    /// Deletes an attendee by ID.
    /// </summary>
    /// <param name="deleteAttendeeCommand">The attendee delete command.</param>
    /// <returns>The deleted attendee details.</returns>
    /// <response code="200">Attendee deleted successfully and returned.</response>
    /// <response code="400">Invalid input.</response>
    /// <response code="404">Attendee not found with the specified ID.</response>
    /// <response code="500">Server error.</response>
    [HttpDelete]
    [ProducesResponseType(typeof(DeletedAttendeeResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(NotFoundProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(InternalServerErrorProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete([FromBody] DeleteAttendeeCommand deleteAttendeeCommand)
    {
        DeletedAttendeeResponse response = await Mediator!.Send(deleteAttendeeCommand);

        return Ok(response);
    }

    /// <summary>
    /// Updates an attendee's check-in.
    /// </summary>
    /// <param name="id">The attendee id.</param>
    /// <returns>Check-In status and success message.</returns>
    /// <response code="200">Attendee's check-in updated successfully and returned.</response>
    /// <response code="400">Invalid input.</response>
    /// <response code="404">Attendee not found with the specified ID.</response>
    /// <response code="500">Server error.</response>
    [HttpPut("{id}/check-in")]
    [ProducesResponseType(typeof(CheckInAttendeeResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(NotFoundProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(InternalServerErrorProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CheckIn([FromRoute] Guid id)
    {
        CheckInAttendeeCommand checkInAttendeeCommand = new() { Id = id };

        CheckInAttendeeResponse response = await Mediator!.Send(checkInAttendeeCommand);

        return Ok(response);
    }

    /// <summary>
    /// Retrieves a specific attendee by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the attendee.</param>
    /// <returns>The details of the attendee.</returns>
    /// <response code="200">Attendee retrieved successfully.</response>
    /// <response code="400">Invalid input.</response>
    /// <response code="404">Attendee not found with the specified ID.</response>
    /// <response code="500">Internal server error.</response>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(GetByIdAttendeeResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(NotFoundProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(InternalServerErrorProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdAttendeeQuery getByIdAttendeeQuery = new() { Id = id };

        GetByIdAttendeeResponse response = await Mediator!.Send(getByIdAttendeeQuery);

        return Ok(response);
    }

    /// <summary>
    /// Retrieves a paginated list of attendees.
    /// </summary>
    /// <param name="pageRequest">Pagination parameters.</param>
    /// <returns>A paginated list of attendees.</returns>
    /// <response code="200">List retrieved successfully.</response>
    /// <response code="400">Invalid input.</response>
    /// <response code="500">Internal server error.</response>
    [HttpGet]
    [ProducesResponseType(typeof(GetListResponse<GetListAttendeeListItemDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(InternalServerErrorProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListAttendeeQuery getListAttendeeQuery = new() { PageRequest = pageRequest };

        GetListResponse<GetListAttendeeListItemDto> response = await Mediator!.Send(getListAttendeeQuery);

        return Ok(response);
    }

    /// <summary>
    /// Retrieves a paginated list of attendees with specified event ID.
    /// </summary>
    /// <param name="pageRequest">Pagination parameters.</param>
    /// <param name="eventId">The unique identifier of the event.</param>
    /// <returns>A paginated list of attendees with specified event ID.</returns>
    /// <response code="200">List retrieved successfully.</response>
    /// <response code="400">Invalid input.</response>
    /// <response code="500">Internal server error.</response>
    [HttpGet("by-event/{eventId}")]
    [ProducesResponseType(typeof(GetListResponse<GetListByEventIdAttendeeListItemDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(InternalServerErrorProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetListByEventId([FromQuery] PageRequest pageRequest, Guid eventId)
    {
        GetListByEventIdAttendeeQuery getListByEventIdAttendeeQuery = new() { PageRequest = pageRequest, EventId = eventId };

        GetListResponse<GetListByEventIdAttendeeListItemDto> response = await Mediator!.Send(getListByEventIdAttendeeQuery);

        return Ok(response);
    }

    /// <summary>
    /// Retrieves a paginated list of attendees with specified ticket ID.
    /// </summary>
    /// <param name="pageRequest">Pagination parameters.</param>
    /// <param name="ticketId">The unique identifier of the ticket.</param>
    /// <returns>A paginated list of attendees with specified ticket ID.</returns>
    /// <response code="200">List retrieved successfully.</response>
    /// <response code="400">Invalid input.</response>
    /// <response code="500">Internal server error.</response>
    [HttpGet("by-ticket/{ticketId}")]
    [ProducesResponseType(typeof(GetListResponse<GetListByEventIdAttendeeListItemDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(InternalServerErrorProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetListByTicketId([FromQuery] PageRequest pageRequest, Guid ticketId)
    {
        GetListByTicketIdAttendeeQuery getListByTicketIdAttendeeQuery = new() { PageRequest = pageRequest, TicketId = ticketId };

        GetListResponse<GetListByTicketIdAttendeeListItemDto> response = await Mediator!.Send(getListByTicketIdAttendeeQuery);

        return Ok(response);
    }

    /// <summary>
    /// Retrieves the total count of attendees for a specific event.
    /// </summary>
    /// <param name="id">The unique identifier of the event to count attendees for.</param>
    /// <returns>Returns the total count of attendees for the specified event.</returns>
    /// <response code="200">>The total count of attendees was retrieved successfully.</response>
    /// <response code="400">Invalid input.</response>
    /// <response code="500">Internal server error.</response>
    [HttpGet("by-event-total-count/{eventId}")]
    [ProducesResponseType(typeof(GetCountByEventAttendeeResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(InternalServerErrorProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetCountByEvent([FromRoute] Guid eventId)
    {
        GetCountByEventAttendeeQuery getCountByEventAttendeeQuery = new() { EventId = eventId };

        GetCountByEventAttendeeResponse response = await Mediator!.Send(getCountByEventAttendeeQuery);

        return Ok(response);
    }
}