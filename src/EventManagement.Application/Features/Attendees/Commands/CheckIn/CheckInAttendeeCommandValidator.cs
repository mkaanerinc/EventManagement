using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Application.Features.Attendees.Commands.CheckIn;

/// <summary>
/// Validator class for <see cref="CheckInAttendeeCommand"/>.
/// Ensures that the attendee ID is valid for updatin an attendee's check in.
/// </summary>
public class CheckInAttendeeCommandValidator : AbstractValidator<CheckInAttendeeCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CheckInAttendeeCommandValidator"/> class.
    /// Defines validation rules for each field of the <see cref="CheckInAttendeeCommand"/>.
    /// </summary>
    public CheckInAttendeeCommandValidator()
    {
        RuleFor(a => a.Id)
            .NotEmpty().WithMessage("Attendee ID must be specified.")
            .NotEqual(Guid.Empty).WithMessage("A valid attendee ID must be provided.");
    }
}