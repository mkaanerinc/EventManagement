using Core.Application.Models.Requests;
using Core.Application.Models.Responses;
using Core.CrossCuttingConcerns.Exceptions.HttpProblemDetails;
using EventManagement.Application.Features.Events.Commands.Create;
using EventManagement.Application.Features.Events.Commands.Delete;
using EventManagement.Application.Features.Events.Commands.Update;
using EventManagement.Application.Features.Events.Queries.GetById;
using EventManagement.Application.Features.Events.Queries.GetList;
using EventManagement.Application.Features.Events.Queries.GetListByDate;
using EventManagement.Application.Features.Events.Queries.GetListByDateRange;
using EventManagement.Application.Features.Events.Queries.GetListOfUpcoming;
using EventManagement.Application.Features.Events.Queries.GetRemainigTicketCountByTicketType;
using EventManagement.Application.Features.Events.Queries.GetRemainingTicketCount;
using EventManagement.Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using ValidationProblemDetails = Core.CrossCuttingConcerns.Exceptions.HttpProblemDetails.ValidationProblemDetails;

namespace EventManagement.Api.Controllers;

/// <summary>
/// API controller for managing events.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class EventsController : BaseController
{
    /// <summary>
    /// Creates a new event.
    /// </summary>
    /// <param name="createEventCommand">The event creation command.</param>
    /// <returns>The created event details.</returns>
    /// <response code="201">Event successfully created.</response>
    /// <response code="400">Invalid input data.</response>
    /// <response code="500">Server error.</response>
    [HttpPost]
    [ProducesResponseType(typeof(CreatedEventResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(InternalServerErrorProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Create([FromBody]  CreateEventCommand createEventCommand)
    {
        CreatedEventResponse response = await Mediator!.Send(createEventCommand);

        return Created(string.Empty,response);
    }

    /// <summary>
    /// Updates an existing event.
    /// </summary>
    /// <param name="updateEventCommand">The event update command.</param>
    /// <returns>The updated event details.</returns>
    /// <response code="200">Event updated successfully and returned.</response>
    /// <response code="400">Invalid input.</response>
    /// <response code="500">Server error.</response>
    [HttpPut]
    [ProducesResponseType(typeof(UpdatedEventResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(InternalServerErrorProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update([FromBody] UpdateEventCommand updateEventCommand)
    {
        UpdatedEventResponse response = await Mediator!.Send(updateEventCommand);

        return Ok(response);
    }

    /// <summary>
    /// Deletes an event by ID.
    /// </summary>
    /// <param name="deleteEventCommand">The event delete command.</param>
    /// <returns>The deleted event details.</returns>
    /// <response code="200">Event deleted successfully and returned.</response>
    /// <response code="400">Invalid input.</response>
    /// <response code="500">Server error.</response>
    [HttpDelete]
    [ProducesResponseType(typeof(DeletedEventResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(InternalServerErrorProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete([FromBody] DeleteEventCommand deleteEventCommand)
    {
        DeletedEventResponse response = await Mediator!.Send(deleteEventCommand);

        return Ok(response);
    }

    /// <summary>
    /// Retrieves a specific event by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the event.</param>
    /// <returns>The details of the event.</returns>
    /// <response code="200">Event retrieved successfully.</response>
    /// <response code="400">Invalid input.</response>
    /// <response code="500">Internal server error.</response>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(GetByIdEventResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(InternalServerErrorProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdEventQuery getByIdEventQuery = new() { Id = id };

        GetByIdEventResponse response = await Mediator!.Send(getByIdEventQuery);

        return Ok(response);
    }

    /// <summary>
    /// Retrieves a paginated list of events.
    /// </summary>
    /// <param name="pageRequest">Pagination parameters.</param>
    /// <returns>A paginated list of events.</returns>
    /// <response code="200">List retrieved successfully.</response>
    /// <response code="400">Invalid input.</response>
    /// <response code="500">Internal server error.</response>
    [HttpGet]
    [ProducesResponseType(typeof(GetListResponse<GetListEventListItemDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(InternalServerErrorProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListEventQuery getListEventQuery = new() { PageRequest = pageRequest };

        GetListResponse<GetListEventListItemDto> response = await Mediator!.Send(getListEventQuery);

        return Ok(response);
    }

    /// <summary>
    /// Gets a paginated list of events filtered by a specific date.
    /// </summary>
    /// <param name="pageRequest">Pagination info such as page number and page size.</param>
    /// <param name="eventAt">The date to filter events by.</param>
    /// <returns>Returns a list of events occurring on the specified date.</returns>
    /// <response code="200">List retrieved successfully.</response>
    /// <response code="400">Invalid input.</response>
    /// <response code="500">Internal server error.</response>
    [HttpGet("by-date")]
    [ProducesResponseType(typeof(GetListResponse<GetListByDateEventListItemDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(InternalServerErrorProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetListByDate([FromQuery] PageRequest pageRequest, [FromQuery] DateTimeOffset eventAt)
    {
        GetListByDateEventQuery getListByDateEventQuery = new() { PageRequest = pageRequest, EventAt =  eventAt};

        GetListResponse<GetListByDateEventListItemDto> response = await Mediator!.Send(getListByDateEventQuery);

        return Ok(response);
    }

    /// <summary>
    /// Gets a paginated list of events filtered by a date range.
    /// </summary>
    /// <param name="pageRequest">Pagination information.</param>
    /// <param name="startAt">Start date of the range.</param>
    /// <param name="endAt">End date of the range.</param>
    /// <returns>Paginated list of events within the date range.</returns>
    /// <response code="200">List retrieved successfully.</response>
    /// <response code="400">Invalid input.</response>
    /// <response code="500">Internal server error.</response>
    [HttpGet("by-date-range")]
    [ProducesResponseType(typeof(GetListResponse<GetListByDateRangeEventListItemDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(InternalServerErrorProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetListByDateRange([FromQuery] PageRequest pageRequest, [FromQuery] DateTimeOffset startAt, [FromQuery] DateTimeOffset endAt)
    {
        GetListByDateRangeEventQuery getListByDateRangeEventQuery = new() { PageRequest = pageRequest, StartAt = startAt, EndAt = endAt };

        GetListResponse<GetListByDateRangeEventListItemDto> response = await Mediator!.Send(getListByDateRangeEventQuery);

        return Ok(response);
    }

    /// <summary>
    /// Gets a paginated list of upcoming events.
    /// </summary>
    /// <param name="pageRequest">Pagination information.</param>
    /// <returns>A list of events that are scheduled to occur in the future.</returns>
    /// <response code="200">List of upcoming events returned successfully.</response>
    /// <response code="400">Invalid input.</response>
    /// <response code="500">Internal server error.</response>
    [HttpGet("of-upcoming")]
    [ProducesResponseType(typeof(GetListResponse<GetListOfUpcomingEventListItemDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(InternalServerErrorProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetListOfUpcoming([FromQuery] PageRequest pageRequest)
    {
        GetListOfUpcomingEventQuery getListOfUpcomingEventQuery = new() { PageRequest = pageRequest };

        GetListResponse<GetListOfUpcomingEventListItemDto> response = await Mediator!.Send(getListOfUpcomingEventQuery);

        return Ok(response);
    }

    /// <summary>
    /// Retrieves the remaining ticket count for a specific event by its ID.
    /// </summary>
    /// <param name="id">The unique identifier of the event.</param>
    /// <returns>The remaining ticket count for the event.</returns>
    /// <response code="200">Successfully retrieved the remaining ticket count.</response>
    /// <response code="400">Invalid input.</response>
    /// <response code="500">An internal server error occurred.</response>
    [HttpGet("{id}/remaining-ticket-count")]
    [ProducesResponseType(typeof(GetRemainingTicketCountEventResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(InternalServerErrorProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetRemainingTicketCount([FromRoute] Guid id)
    {
        GetRemainingTicketCountEventQuery getRemainingTicketCountEventQuery = new() { EventId = id };

        GetRemainingTicketCountEventResponse response = await Mediator!.Send(getRemainingTicketCountEventQuery);

        return Ok(response);
    }

    /// <summary>
    /// Retrieves the remaining ticket count for a specific event by its ID and ticket type.
    /// </summary>
    /// <param name="id">The unique identifier of the event.</param>
    /// <param name="ticketType">The type of ticket for which the remaining count is requested.</param>
    /// <returns>The remaining ticket count for the specified event and ticket type.</returns>
    /// <response code="200">Successfully retrieved the remaining ticket count for the specified ticket type.</response>
    /// <response code="400">Invalid input.</response>
    /// <response code="500">An internal server error occurred.</response>
    [HttpGet("{id}/remaining-ticket-count-by-ticket-type")]
    [ProducesResponseType(typeof(GetRemainigTicketCountByTicketTypeEventResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(InternalServerErrorProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetRemainingTicketCountByTicketType([FromRoute] Guid id, [FromQuery] TicketType ticketType)
    {
        GetRemainigTicketCountByTicketTypeEventQuery getRemainigTicketCountByTicketTypeEventQuery = new() { EventId = id, TicketType = ticketType };

        GetRemainigTicketCountByTicketTypeEventResponse response = await Mediator!.Send(getRemainigTicketCountByTicketTypeEventQuery);

        return Ok(response);
    }
}