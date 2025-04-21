using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Application.Features.Attendees.Queries.GetListByTicketId;

/// <summary>
/// Validator class for <see cref="GetListByTicketIdAttendeeQuery"/>.
/// Ensures that pagination parameters in <see cref="PageRequest"/> are within valid bounds.
/// Ensures the ticket ID is valid when retrieving attendee by ticket ID.
/// </summary>
public class GetListByTicketIdAttendeeQueryValidator : AbstractValidator<GetListByTicketIdAttendeeQuery>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetListByTicketIdAttendeeQueryValidator"/> class.
    /// Defines validation rules for each field of the <see cref="GetListByTicketIdAttendeeQuery"/>.
    /// </summary>
    public GetListByTicketIdAttendeeQueryValidator()
    {
        RuleFor(a => a.TicketId)
            .NotEmpty().WithMessage("Ticket ID cannot be empty.")
            .NotEqual(Guid.Empty).WithMessage("A valid ticket ID must be provided.");

        RuleFor(a => a.PageRequest.PageIndex)
             .GreaterThan(0).WithMessage("Page number must be greater than 0.");

        RuleFor(a => a.PageRequest.PageSize)
            .GreaterThan(0).WithMessage("Page size must be greater than 0.")
            .LessThanOrEqualTo(100).WithMessage("Page size can be at most 100.");
    }
}