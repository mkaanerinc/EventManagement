using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Application.Features.Attendees.Queries.GetCountByEvent;

/// <summary>
/// Validator class for <see cref="GetRemainingTicketCountEventQuery"/>.
/// Ensures the event ID is valid for retrieving the total count of attendees.
/// </summary>
public class GetCountByEventAttendeeQueryValidator : AbstractValidator<GetCountByEventAttendeeQuery>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetCountByEventAttendeeQueryValidator"/> class.
    /// Defines validation rules for the EventId property.
    /// </summary>
    public GetCountByEventAttendeeQueryValidator()
    {
        RuleFor(a => a.EventId)
            .NotEmpty().WithMessage("Event ID cannot be empty.")
            .NotEqual(Guid.Empty).WithMessage("A valid event ID must be provided.");
    }
}
