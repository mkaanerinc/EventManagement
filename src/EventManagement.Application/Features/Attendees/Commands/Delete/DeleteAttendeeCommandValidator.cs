using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Application.Features.Attendees.Commands.Delete;

/// <summary>
/// Validator class for <see cref="DeleteAttendeeCommand"/>.
/// Ensures the attendee ID is valid for deleting an attendee.
/// </summary>
public class DeleteAttendeeCommandValidator : AbstractValidator<DeleteAttendeeCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteAttendeeCommandValidator"/> class.
    /// Defines validation rules for each field of the <see cref="DeleteAttendeeCommand"/>.
    /// </summary>
    public DeleteAttendeeCommandValidator()
    {
        RuleFor(a => a.Id)
            .NotEmpty().WithMessage("Attendee ID must be specified.")
            .NotEqual(Guid.Empty).WithMessage("A valid attendee ID must be provided.");
    }
}