using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Application.Features.Attendees.Queries.GetById;

/// <summary>
/// Validator class for <see cref="GetByIdAttendeeQuery"/>.
/// Ensures the attendee ID is valid when retrieving an attendee by ID.
/// </summary>
public class GetByIdAttendeeQueryValidator : AbstractValidator<GetByIdAttendeeQuery>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetByIdAttendeeQueryValidator"/> class.
    /// Defines validation rules for each field of the <see cref="GetByIdAttendeeQuery"/>.
    /// </summary>
    public GetByIdAttendeeQueryValidator()
    {
        RuleFor(a => a.Id)
            .NotEmpty().WithMessage("Attendee ID cannot be empty.")
            .NotEqual(Guid.Empty).WithMessage("A valid attendee ID must be provided.");
    }
}