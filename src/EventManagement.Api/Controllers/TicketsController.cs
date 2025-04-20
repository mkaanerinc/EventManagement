using Core.Application.Models.Requests;
using Core.Application.Models.Responses;
using Core.CrossCuttingConcerns.Exceptions.HttpProblemDetails;
using EventManagement.Application.Features.Tickets.Commands.Create;
using EventManagement.Application.Features.Tickets.Commands.Delete;
using EventManagement.Application.Features.Tickets.Commands.Update;
using EventManagement.Application.Features.Tickets.Queries.GetAvailable;
using EventManagement.Application.Features.Tickets.Queries.GetByEventId;
using EventManagement.Application.Features.Tickets.Queries.GetById;
using EventManagement.Application.Features.Tickets.Queries.GetList;
using EventManagement.Application.Features.Tickets.Queries.GetSaleSummary;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ValidationProblemDetails = Core.CrossCuttingConcerns.Exceptions.HttpProblemDetails.ValidationProblemDetails;

namespace EventManagement.Api.Controllers;

/// <summary>
/// API controller for managing tickets.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class TicketsController : BaseController
{
    /// <summary>
    /// Creates a new ticket.
    /// </summary>
    /// <param name="createTicketCommand">The ticket creation command.</param>
    /// <returns>The created ticket details.</returns>
    /// <response code="201">Ticket successfully created.</response>
    /// <response code="400">Invalid input data.</response>
    /// <response code="500">Server error.</response>
    [HttpPost]
    [ProducesResponseType(typeof(CreatedTicketResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(InternalServerErrorProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Create([FromBody] CreateTicketCommand createTicketCommand)
    {
        CreatedTicketResponse response = await Mediator!.Send(createTicketCommand);

        return Created(string.Empty, response);
    }

    /// <summary>
    /// Updates an existing ticket.
    /// </summary>
    /// <param name="updateTicketCommand">The ticket update command.</param>
    /// <returns>The updated ticket details.</returns>
    /// <response code="200">Ticket updated successfully and returned.</response>
    /// <response code="400">Invalid input.</response>
    /// <response code="404">Ticket not found with the specified ID.</response>
    /// <response code="500">Server error.</response>
    [HttpPut]
    [ProducesResponseType(typeof(UpdatedTicketResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(NotFoundProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(InternalServerErrorProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update([FromBody] UpdateTicketCommand updateTicketCommand)
    {
        UpdatedTicketResponse response = await Mediator!.Send(updateTicketCommand);

        return Ok(response);
    }

    /// <summary>
    /// Deletes an ticket by ID.
    /// </summary>
    /// <param name="deleteTicketCommand">The ticket delete command.</param>
    /// <returns>The deleted ticket details.</returns>
    /// <response code="200">Ticket deleted successfully and returned.</response>
    /// <response code="400">Invalid input.</response>
    /// <response code="404">Ticket not found with the specified ID.</response>
    /// <response code="500">Server error.</response>
    [HttpDelete]
    [ProducesResponseType(typeof(DeletedTicketResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(NotFoundProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(InternalServerErrorProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete([FromBody] DeleteTicketCommand deleteTicketCommand)
    {
        DeletedTicketResponse response = await Mediator!.Send(deleteTicketCommand);

        return Ok(response);
    }

    /// <summary>
    /// Retrieves a specific ticket by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the ticket.</param>
    /// <returns>The details of the ticket.</returns>
    /// <response code="200">Ticket retrieved successfully.</response>
    /// <response code="400">Invalid input.</response>
    /// <response code="404">Ticket not found with the specified ID.</response>
    /// <response code="500">Internal server error.</response>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(GetByIdTicketResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(NotFoundProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(InternalServerErrorProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdTicketQuery getByIdTicketQuery = new() { Id = id };

        GetByIdTicketResponse response = await Mediator!.Send(getByIdTicketQuery);

        return Ok(response);
    }

    /// <summary>
    /// Retrieves a paginated list of tickets.
    /// </summary>
    /// <param name="pageRequest">Pagination parameters.</param>
    /// <returns>A paginated list of tickets.</returns>
    /// <response code="200">List retrieved successfully.</response>
    /// <response code="400">Invalid input.</response>
    /// <response code="500">Internal server error.</response>
    [HttpGet]
    [ProducesResponseType(typeof(GetListResponse<GetListTicketListItemDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(InternalServerErrorProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListTicketQuery getListTicketQuery = new() { PageRequest = pageRequest };

        GetListResponse<GetListTicketListItemDto> response = await Mediator!.Send(getListTicketQuery);

        return Ok(response);
    }

    /// <summary>
    /// Retrieves a paginated list of available tickets for a specific event.
    /// </summary>
    /// <param name="pageRequest">Pagination parameters.</param>
    /// <param name="eventId">The unique identifier of the event.</param>
    /// <returns>A paginated list of available tickets.</returns>
    /// <response code="200">List retrieved successfully.</response>
    /// <response code="400">Invalid input provided.</response>
    /// <response code="404">No tickets found for the specified event.</response>
    /// <response code="500">Internal server error occurred.</response>
    [HttpGet("available/{eventId}")]
    [ProducesResponseType(typeof(GetListResponse<GetAvailableTicketListItemDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(InternalServerErrorProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAvailable([FromQuery] PageRequest pageRequest, Guid eventId)
    {
        GetAvailableTicketQuery getAvailableTicketQuery = new() { PageRequest = pageRequest, EventId = eventId };

        GetListResponse<GetAvailableTicketListItemDto> response = await Mediator!.Send(getAvailableTicketQuery);

        return Ok(response);
    }

    /// <summary>
    /// Retrieves ticket details for a specific event.
    /// </summary>
    /// <param name="eventId">The unique identifier of the event.</param>
    /// <returns>Details of the tickets associated with the specified event.</returns>
    /// <response code="200">Ticket details retrieved successfully.</response>
    /// <response code="400">Invalid event ID provided.</response>
    /// <response code="404">No tickets found for the specified event.</response>
    /// <response code="500">Internal server error occurred.</response>
    [HttpGet("by-event/{eventId}")]
    [ProducesResponseType(typeof(GetByEventIdTicketResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(NotFoundProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(InternalServerErrorProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetByEventId([FromRoute] Guid eventId)
    {
        GetByEventIdTicketQuery getByEventIdTicketQuery = new() { EventId = eventId };

        GetByEventIdTicketResponse response = await Mediator!.Send(getByEventIdTicketQuery);

        return Ok(response);
    }

    /// <summary>
    /// Retrieves the sale summary for tickets of a specific event.
    /// </summary>
    /// <param name="eventId">The unique identifier of the event.</param>
    /// <returns>A summary of ticket sales for the specified event.</returns>
    /// <response code="200">Sale summary retrieved successfully.</response>
    /// <response code="400">Invalid event ID provided.</response>
    /// <response code="404">Sale summary not found for the specified event.</response>
    /// <response code="500">Internal server error occurred.</response>
    [HttpGet("{eventId}/sale-summary")]
    [ProducesResponseType(typeof(GetSaleSummaryTicketResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(NotFoundProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(InternalServerErrorProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetSaleSummary([FromRoute] Guid eventId)
    {
        GetSaleSummaryTicketQuery getSaleSummaryTicketQuery = new() { EventId = eventId };

        GetSaleSummaryTicketResponse response = await Mediator!.Send(getSaleSummaryTicketQuery);

        return Ok(response);
    }
}