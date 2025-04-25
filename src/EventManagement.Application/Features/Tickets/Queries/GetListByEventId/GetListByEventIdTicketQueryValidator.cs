using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Application.Features.Tickets.Queries.GetListByEventId;

/// <summary>
/// Validator class for <see cref="GetListByEventIdTicketQuery"/>.
/// Ensures that pagination parameters in <see cref="PageRequest"/> are within valid bounds.
/// Ensures the event ID is valid when retrieving a ticket by event ID.
/// </summary>
public class GetListByEventIdTicketQueryValidator : AbstractValidator<GetListByEventIdTicketQuery>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetListByEventIdTicketQueryValidator"/> class.
    /// Defines validation rules for each field of the <see cref="GetListByEventIdTicketQuery"/>.
    /// </summary>
    public GetListByEventIdTicketQueryValidator()
    {
        RuleFor(t => t.EventId)
            .NotEmpty().WithMessage("Event ID cannot be empty.")
            .NotEqual(Guid.Empty).WithMessage("A valid event ID must be provided.");

        RuleFor(t => t.PageRequest.PageIndex)
             .GreaterThan(0).WithMessage("Page number must be greater than 0.");

        RuleFor(t => t.PageRequest.PageSize)
            .GreaterThan(0).WithMessage("Page size must be greater than 0.")
            .LessThanOrEqualTo(100).WithMessage("Page size can be at most 100.");
    }
}