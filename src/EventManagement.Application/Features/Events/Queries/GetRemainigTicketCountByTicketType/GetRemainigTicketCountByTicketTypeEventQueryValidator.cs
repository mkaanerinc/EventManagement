using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Application.Features.Events.Queries.GetRemainigTicketCountByTicketType;

/// <summary>
/// Validator class for <see cref="GetRemainigTicketCountByTicketTypeEventQuery"/>.
/// Ensures the event ID is valid and the ticket type is properly specified.
/// </summary>
public class GetRemainigTicketCountByTicketTypeEventQueryValidator : AbstractValidator<GetRemainigTicketCountByTicketTypeEventQuery>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetRemainigTicketCountByTicketTypeEventQueryValidator"/> class.
    /// Defines validation rules for the EventId and TicketType properties.
    /// </summary>
    public GetRemainigTicketCountByTicketTypeEventQueryValidator()
    {
        RuleFor(q => q.EventId)
            .NotEmpty().WithMessage("Etkinlik ID'si boş olamaz.")
            .NotEqual(Guid.Empty).WithMessage("Geçerli bir etkinlik ID'si girilmelidir.");

        RuleFor(q => q.TicketType)
            .IsInEnum().WithMessage("Geçerli bir bilet türü seçilmelidir.");
    }
}
