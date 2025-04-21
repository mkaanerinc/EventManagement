using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Application.Features.Tickets.Commands.SellTicket;

/// <summary>
/// Validator class for <see cref="SellTicketCommand"/>.
/// Ensures that the input data is valid for selling a ticket.
/// </summary>
public class SellTicketCommandValidator : AbstractValidator<SellTicketCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SellTicketCommandValidator"/> class.
    /// Defines validation rules for each field of the <see cref="SellTicketCommand"/>.
    /// </summary>
    public SellTicketCommandValidator()
    {
        RuleFor(t => t.Id)
            .NotEmpty().WithMessage("Ticket ID must be specified.")
            .NotEqual(Guid.Empty).WithMessage("A valid ticket ID must be provided.");

        RuleFor(t => t.FullName)
            .NotEmpty().WithMessage("Full name cannot be empty.")
            .MaximumLength(100).WithMessage("Full name cannot be longer than 100 characters.")
            .MinimumLength(2).WithMessage("Full name must be at least 2 characters long.");

        RuleFor(t => t.Email)
            .NotEmpty().WithMessage("Email cannot be empty.")
            .EmailAddress().WithMessage("A valid email address is required.")
            .MaximumLength(100).WithMessage("Email cannot be longer than 100 characters.");
    }
}