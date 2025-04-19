using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Application.Features.Tickets.Commands.Delete;

/// <summary>
/// Validator class for <see cref="DeleteTicketCommand"/>.
/// Ensures the ticket ID is valid for deleting a ticket.
/// </summary>
public class DeleteTicketCommandValidator : AbstractValidator<DeleteTicketCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteTicketCommandValidator"/> class.
    /// Defines validation rules for each field of the <see cref="DeleteTicketCommand"/>.
    /// </summary>
    public DeleteTicketCommandValidator()
    {
        RuleFor(t => t.Id)
            .NotEmpty().WithMessage("Bilet ID'si boş olamaz.")
            .NotEqual(Guid.Empty).WithMessage("Geçerli bir bilet ID'si girilmelidir.");
    }
}