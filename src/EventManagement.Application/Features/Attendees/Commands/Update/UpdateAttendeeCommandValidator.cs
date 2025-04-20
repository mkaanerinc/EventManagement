using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Application.Features.Attendees.Commands.Update;

/// <summary>
/// Validator class for <see cref="UpdateAttendeeCommand"/>.
/// Ensures that the input data is valid for creating an attendee.
/// </summary>
public class UpdateAttendeeCommandValidator : AbstractValidator<UpdateAttendeeCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateAttendeeCommandValidator"/> class.
    /// Defines validation rules for each field of the <see cref="UpdateAttendeeCommand"/>.
    /// </summary>
    public UpdateAttendeeCommandValidator()
    {
        RuleFor(a => a.Id)
            .NotEmpty().WithMessage("Attendee ID must be specified.")
            .NotEqual(Guid.Empty).WithMessage("A valid attendee ID must be provided.");

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