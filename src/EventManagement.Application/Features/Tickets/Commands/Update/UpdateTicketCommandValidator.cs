using EventManagement.Domain.Enums;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Application.Features.Tickets.Commands.Update;

/// <summary>
/// Validator class for <see cref="UpdateTicketCommand"/>.
/// Ensures that the input data is valid for updating a ticket.
/// </summary>
public class UpdateTicketCommandValidator : AbstractValidator<UpdateTicketCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateTicketCommandValidator"/> class.
    /// Defines validation rules for each field of the <see cref="UpdateTicketCommand"/>.
    /// </summary>
    public UpdateTicketCommandValidator()
    {
        RuleFor(t => t.Id)
            .NotEmpty().WithMessage("Ticket ID cannot be empty.")
            .NotEqual(Guid.Empty).WithMessage("A valid ticket ID must be provided.");

        RuleFor(t => t.EventId)
            .NotEmpty().WithMessage("Event ID must be specified.");

        RuleFor(t => t.TicketType)
            .Must(BeAValidTicketType).WithMessage("A valid ticket type must be selected.");

        RuleFor(t => t.Price)
            .NotNull().WithMessage("Price must be specified.")
            .GreaterThan(0).WithMessage("Price must be greater than zero.");

        RuleFor(t => t.QuantityAvailable)
            .NotNull().WithMessage("Available quantity must be specified.")
            .GreaterThanOrEqualTo(0).WithMessage("Quantity cannot be negative.");

        RuleFor(t => t.QuantitySold)
            .GreaterThanOrEqualTo(0).WithMessage("Sold quantity cannot be negative.");
    }

    /// <summary>
    /// Checks whether the given <see cref="TicketType"/> value is defined in the enum.
    /// </summary>
    /// <param name="ticketType">The ticket type value to validate.</param>
    /// <returns>
    /// <c>true</c> if the value is a defined member of the <see cref="TicketType"/> enum; otherwise, <c>false</c>.
    /// </returns>
    private bool BeAValidTicketType(TicketType ticketType)
    {
        return Enum.IsDefined(typeof(TicketType), ticketType);
    }
}
