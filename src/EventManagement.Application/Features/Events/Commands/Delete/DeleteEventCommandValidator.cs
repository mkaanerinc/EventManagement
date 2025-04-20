using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Application.Features.Events.Commands.Delete;

/// <summary>
/// Validator class for <see cref="DeleteEventCommand"/>.
/// Ensures the event ID is valid for deleting an event.
/// </summary>
public class DeleteEventCommandValidator : AbstractValidator<DeleteEventCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteEventCommandValidator"/> class.
    /// Defines validation rules for each field of the <see cref="DeleteEventCommand"/>.
    /// </summary>
    public DeleteEventCommandValidator()
    {
        RuleFor(e => e.Id)
            .NotEmpty().WithMessage("Event ID cannot be empty.")
            .NotEqual(Guid.Empty).WithMessage("A valid event ID must be provided.");
    }
}