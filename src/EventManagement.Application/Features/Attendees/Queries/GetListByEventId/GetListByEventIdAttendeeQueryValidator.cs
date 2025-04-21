using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Application.Features.Attendees.Queries.GetListByEventId;

/// <summary>
/// Validator class for <see cref="GetListByEventIdAttendeeQuery"/>.
/// Ensures that pagination parameters in <see cref="PageRequest"/> are within valid bounds.
/// Ensures the event ID is valid when retrieving attendee by event ID.
/// </summary>
public class GetListByEventIdAttendeeQueryValidator : AbstractValidator<GetListByEventIdAttendeeQuery>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetListByEventIdAttendeeQueryValidator"/> class.
    /// Defines validation rules for each field of the <see cref="GetListByEventIdAttendeeQuery"/>.
    /// </summary>
    public GetListByEventIdAttendeeQueryValidator()
    {
        RuleFor(a => a.EventId)
            .NotEmpty().WithMessage("Event ID cannot be empty.")
            .NotEqual(Guid.Empty).WithMessage("A valid event ID must be provided.");

        RuleFor(a => a.PageRequest.PageIndex)
             .GreaterThan(0).WithMessage("Page number must be greater than 0.");

        RuleFor(a => a.PageRequest.PageSize)
            .GreaterThan(0).WithMessage("Page size must be greater than 0.")
            .LessThanOrEqualTo(100).WithMessage("Page size can be at most 100.");
    }
}