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
        RuleFor(e => e.Id)
            .NotEmpty().WithMessage("Bilet ID'si boş olamaz.")
            .NotEqual(Guid.Empty).WithMessage("Geçerli bir bilet ID'si girilmelidir.");

        RuleFor(x => x.EventId)
            .NotEmpty().WithMessage("Etkinlik kimliği belirtilmelidir.");

        RuleFor(x => x.TicketType)
            .Must(BeAValidTicketType).WithMessage("Geçerli bir bilet türü seçilmelidir.");

        RuleFor(x => x.Price)
            .NotNull().WithMessage("Fiyat belirtilmelidir.")
            .GreaterThan(0).WithMessage("Fiyat sıfırdan büyük olmalıdır.");

        RuleFor(x => x.QuantityAvailable)
            .NotNull().WithMessage("Satışa sunulan miktar belirtilmelidir.")
            .GreaterThanOrEqualTo(0).WithMessage("Miktar negatif olamaz.");

        RuleFor(x => x.QuantitySold)
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
