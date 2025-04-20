using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Application.Features.Attendees.Commands.Create;

/// <summary>
/// Validator class for <see cref="CreateAttendeeCommand"/>.
/// Ensures that the input data is valid for creating an attendee.
/// </summary>
public class CreateAttendeeCommandValidator : AbstractValidator<CreateAttendeeCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateAttendeeCommandValidator"/> class.
    /// Defines validation rules for each field of the <see cref="CreateAttendeeCommand"/>.
    /// </summary>
    public CreateAttendeeCommandValidator()
    {
        RuleFor(a => a.TicketId)
            .NotEmpty().WithMessage("Ticket ID must be specified.");

        RuleFor(a => a.FullName)
            .NotEmpty().WithMessage("Full name cannot be empty.")
            .MaximumLength(100).WithMessage("Full name cannot be longer than 100 characters.")
            .MinimumLength(2).WithMessage("Full name must be at least 2 characters long.");

        RuleFor(a => a.Email)
            .NotEmpty().WithMessage("Email cannot be empty.")
            .EmailAddress().WithMessage("A valid email address is required.")
            .MaximumLength(100).WithMessage("Email cannot be longer than 100 characters.");

        RuleFor(a => a.PurchasedAt)
            .NotEmpty().WithMessage("Purchase date must be specified.")
            .Must(BePastOrPresent).WithMessage("Purchase date cannot be in the future.");
    }

    /// <summary>
    /// Checks if the purchase date is in the past or present.
    /// </summary>
    /// <param name="purchaseAt">The purchase date to validate.</param>
    /// <returns>True if the purchase date is less than or equal to the current UTC time; otherwise, false.</returns>
    private bool BePastOrPresent(DateTimeOffset purchaseAt)
    {
        return purchaseAt <= DateTimeOffset.UtcNow;
    }
}