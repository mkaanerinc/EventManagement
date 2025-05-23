﻿using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Application.Features.Events.Commands.Create;

/// <summary>
/// Validator class for <see cref="CreateEventCommand"/>.
/// Ensures that the input data is valid for creating an event.
/// </summary>
public class CreateEventCommandValidator : AbstractValidator<CreateEventCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateEventCommandValidator"/> class.
    /// Defines validation rules for each field of the <see cref="CreateEventCommand"/>.
    /// </summary>
    public CreateEventCommandValidator()
    {
        RuleFor(e => e.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MinimumLength(3).WithMessage("Title must be at least 3 characters long.");

        RuleFor(e => e.Description)
            .NotEmpty().WithMessage("Description is required.")
            .MinimumLength(10).WithMessage("Description must be at least 10 characters long.");

        RuleFor(e => e.Location)
            .NotEmpty().WithMessage("Location is required.")
            .MinimumLength(3).WithMessage("Location must be at least 3 characters long.");

        RuleFor(e => e.OrganizerName)
            .NotEmpty().WithMessage("Organizer name is required.")
            .MinimumLength(2).WithMessage("Organizer name must be at least 2 characters long.");

        RuleFor(e => e.EventAt)
            .NotEmpty().WithMessage("Event date is required.")
            .Must(BeAFutureDate).WithMessage("Event date must be in the future.");

        RuleFor(e => e.TotalCapacity)
            .NotEmpty().WithMessage("Total capacity is required.")
            .GreaterThanOrEqualTo(0).WithMessage("Total capacity cannot be negative.");
    }

    /// <summary>
    /// Checks if the given date is a future date.
    /// </summary>
    /// <param name="date">The date to check.</param>
    /// <returns>True if the date is in the future, otherwise false.</returns>
    private bool BeAFutureDate(DateTimeOffset date)
    {
        return date >= DateTimeOffset.UtcNow;
    }
}
