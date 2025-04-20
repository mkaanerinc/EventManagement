using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Application.Features.Tickets.Queries.GetByEventId;

/// <summary>
/// Validator class for <see cref="GetByEventIdTicketQuery"/>.
/// Ensures the event ID is valid when retrieving a ticket by event ID.
/// </summary>
public class GetByEventIdTicketQueryValidator : AbstractValidator<GetByEventIdTicketQuery>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetByEventIdTicketQueryValidator"/> class.
    /// Defines validation rules for each field of the <see cref="GetByEventIdTicketQuery"/>.
    /// </summary>
    public GetByEventIdTicketQueryValidator()
    {
        RuleFor(t => t.EventId)
            .NotEmpty().WithMessage("Etkinlik ID'si boş olamaz.")
            .NotEqual(Guid.Empty).WithMessage("Geçerli bir etkinlik ID'si girilmelidir.");
    }
}