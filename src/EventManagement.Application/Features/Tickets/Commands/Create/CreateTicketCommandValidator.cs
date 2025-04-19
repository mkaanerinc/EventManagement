using EventManagement.Domain.Enums;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Application.Features.Tickets.Commands.Create;

/// <summary>
/// Validator class for <see cref="CreateTicketCommand"/>.
/// Ensures that the input data is valid for creating a ticket.
/// </summary>
public class CreateTicketCommandValidator : AbstractValidator<CreateTicketCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateTicketCommandValidator"/> class.
    /// Defines validation rules for each field of the <see cref="CreateTicketCommand"/>.
    /// </summary>
    public CreateTicketCommandValidator()
    {
        RuleFor(t => t.EventId)
            .NotEmpty().WithMessage("Etkinlik kimliği belirtilmelidir.");

        RuleFor(t => t.TicketType)
            .Must(BeAValidTicketType).WithMessage("Geçerli bir bilet türü seçilmelidir.");

        RuleFor(t => t.Price)
            .NotNull().WithMessage("Fiyat belirtilmelidir.")
            .GreaterThan(0).WithMessage("Fiyat sıfırdan büyük olmalıdır.");

        RuleFor(t => t.QuantityAvailable)
            .NotNull().WithMessage("Satışa sunulan miktar belirtilmelidir.")
            .GreaterThanOrEqualTo(0).WithMessage("Miktar negatif olamaz.");

        RuleFor(t => t.QuantitySold)
            .GreaterThanOrEqualTo(0).WithMessage("Satılan miktar negatif olamaz.");
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
