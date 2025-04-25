using Core.Application.Models.Requests;
using Core.Application.Models.Responses;
using Core.CrossCuttingConcerns.Exceptions.HttpProblemDetails;
using EventManagement.Application.Features.Reports.Commands.Create;
using EventManagement.Application.Features.Reports.Commands.Delete;
using EventManagement.Application.Features.Reports.Commands.Update;
using EventManagement.Application.Features.Reports.Queries.GetById;
using EventManagement.Application.Features.Reports.Queries.GetList;
using EventManagement.Application.Features.Reports.Queries.GetListByEventId;
using EventManagement.Application.Features.Reports.Queries.GetListCompleted;
using EventManagement.Application.Features.Reports.Queries.GetListFailed;
using EventManagement.Application.Features.Reports.Queries.GetListPending;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ValidationProblemDetails = Core.CrossCuttingConcerns.Exceptions.HttpProblemDetails.ValidationProblemDetails;

namespace EventManagement.Api.Controllers;

/// <summary>
/// API controller for managing reports.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class ReportsController : BaseController
{
    /// <summary>
    /// Creates a new report.
    /// </summary>
    /// <param name="createReportCommand">The report creation command.</param>
    /// <returns>The created report details.</returns>
    /// <response code="201">Report successfully created.</response>
    /// <response code="400">Invalid input data.</response>
    /// <response code="500">Server error.</response>
    [HttpPost]
    [ProducesResponseType(typeof(CreatedReportResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(InternalServerErrorProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Create([FromBody] CreateReportCommand createReportCommand)
    {
        CreatedReportResponse response = await Mediator!.Send(createReportCommand);

        return Created(string.Empty, response);
    }

    /// <summary>
    /// Updates an existing report.
    /// </summary>
    /// <param name="updateReportCommand">The report update command.</param>
    /// <returns>The updated report details.</returns>
    /// <response code="200">Report updated successfully and returned.</response>
    /// <response code="400">Invalid input.</response>
    /// <response code="404">Report not found with the specified ID.</response>
    /// <response code="500">Server error.</response>
    [HttpPut]
    [ProducesResponseType(typeof(UpdatedReportResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(NotFoundProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(InternalServerErrorProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update([FromBody] UpdateReportCommand updateReportCommand)
    {
        UpdatedReportResponse response = await Mediator!.Send(updateReportCommand);

        return Ok(response);
    }

    /// <summary>
    /// Deletes a report by ID.
    /// </summary>
    /// <param name="deleteReportCommand">The report delete command.</param>
    /// <returns>The deleted report details.</returns>
    /// <response code="200">Report deleted successfully and returned.</response>
    /// <response code="400">Invalid input.</response>
    /// <response code="404">Report not found with the specified ID.</response>
    /// <response code="500">Server error.</response>
    [HttpDelete]
    [ProducesResponseType(typeof(DeletedReportResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(NotFoundProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(InternalServerErrorProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete([FromBody] DeleteReportCommand deleteReportCommand)
    {
        DeletedReportResponse response = await Mediator!.Send(deleteReportCommand);

        return Ok(response);
    }

    /// <summary>
    /// Retrieves a specific report by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the report.</param>
    /// <returns>The details of the report.</returns>
    /// <response code="200">Report retrieved successfully.</response>
    /// <response code="400">Invalid input.</response>
    /// <response code="404">Report not found with the specified ID.</response>
    /// <response code="500">Internal server error.</response>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(GetByIdReportResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(NotFoundProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(InternalServerErrorProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdReportQuery getByIdReportQuery = new() { Id = id };

        GetByIdReportResponse response = await Mediator!.Send(getByIdReportQuery);

        return Ok(response);
    }

    /// <summary>
    /// Retrieves a paginated list of reports.
    /// </summary>
    /// <param name="pageRequest">Pagination parameters.</param>
    /// <returns>A paginated list of reports.</returns>
    /// <response code="200">List retrieved successfully.</response>
    /// <response code="400">Invalid input.</response>
    /// <response code="500">Internal server error.</response>
    [HttpGet]
    [ProducesResponseType(typeof(GetListResponse<GetListReportListItemDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(InternalServerErrorProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListReportQuery getListReportQuery = new() { PageRequest = pageRequest };

        GetListResponse<GetListReportListItemDto> response = await Mediator!.Send(getListReportQuery);

        return Ok(response);
    }

    /// <summary>
    /// Retrieves a paginated list of reports with the specified pending report status.
    /// </summary>
    /// <param name="pageRequest">Pagination parameters.</param>
    /// <returns>A paginated list of reports with the specified pending report status.</returns>
    /// <response code="200">List retrieved successfully.</response>
    /// <response code="400">Invalid input.</response>
    /// <response code="500">Internal server error.</response>
    [HttpGet("pending")]
    [ProducesResponseType(typeof(GetListResponse<GetListPendingReportListItemDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(InternalServerErrorProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetListPending([FromQuery] PageRequest pageRequest)
    {
        GetListPendingReportQuery getListPendingReportQuery = new() { PageRequest = pageRequest };

        GetListResponse<GetListPendingReportListItemDto> response = await Mediator!.Send(getListPendingReportQuery);

        return Ok(response);
    }

    /// <summary>
    /// Retrieves a paginated list of reports with the specified failed report status.
    /// </summary>
    /// <param name="pageRequest">Pagination parameters.</param>
    /// <returns>A paginated list of reports with the specified failed report status.</returns>
    /// <response code="200">List retrieved successfully.</response>
    /// <response code="400">Invalid input.</response>
    /// <response code="500">Internal server error.</response>
    [HttpGet("failed")]
    [ProducesResponseType(typeof(GetListResponse<GetListFailedReportListItemDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(InternalServerErrorProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetListFailed([FromQuery] PageRequest pageRequest)
    {
        GetListFailedReportQuery getListFailedReportQuery = new() { PageRequest = pageRequest };

        GetListResponse<GetListFailedReportListItemDto> response = await Mediator!.Send(getListFailedReportQuery);

        return Ok(response);
    }

    /// <summary>
    /// Retrieves a paginated list of reports with the specified completed report status.
    /// </summary>
    /// <param name="pageRequest">Pagination parameters.</param>
    /// <returns>A paginated list of reports with the specified completed report status.</returns>
    /// <response code="200">List retrieved successfully.</response>
    /// <response code="400">Invalid input.</response>
    /// <response code="500">Internal server error.</response>
    [HttpGet("completed")]
    [ProducesResponseType(typeof(GetListResponse<GetListCompletedReportListItemDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(InternalServerErrorProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetListCompleted([FromQuery] PageRequest pageRequest)
    {
        GetListCompletedReportQuery getListCompletedReportQuery = new() { PageRequest = pageRequest };

        GetListResponse<GetListCompletedReportListItemDto> response = await Mediator!.Send(getListCompletedReportQuery);

        return Ok(response);
    }

    /// <summary>
    /// Retrieves a paginated list of reports with specified event ID.
    /// </summary>
    /// <param name="pageRequest">Pagination parameters.</param>
    /// <param name="eventId">The unique identifier of the event.</param>
    /// <returns>A paginated list of reports with specified event ID.</returns>
    /// <response code="200">List retrieved successfully.</response>
    /// <response code="400">Invalid input.</response>
    /// <response code="500">Internal server error.</response>
    [HttpGet("by-event/{eventId}")]
    [ProducesResponseType(typeof(GetListResponse<GetListByEventIdReportListItemDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(InternalServerErrorProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetListByEventId([FromQuery] PageRequest pageRequest, Guid eventId)
    {
        GetListByEventIdReportQuery getListByEventIdReportQuery = new() { PageRequest = pageRequest, EventId = eventId };

        GetListResponse<GetListByEventIdReportListItemDto> response = await Mediator!.Send(getListByEventIdReportQuery);

        return Ok(response);
    }
}