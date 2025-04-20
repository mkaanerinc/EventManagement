using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Application.Features.Tickets.Queries.GetSaleSummary;

/// <summary>
/// Validator class for <see cref="GetSaleSummaryTicketQuery"/>.
/// Ensures the event ID is valid when retrieving a ticket by event ID.
/// </summary>
public class GetSaleSummaryTicketQueryValidator : AbstractValidator<GetSaleSummaryTicketQuery>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetSaleSummaryTicketQueryValidator"/> class.
    /// Defines validation rules for each field of the <see cref="GetSaleSummaryTicketQuery"/>.
    /// </summary>
    public GetSaleSummaryTicketQueryValidator()
    {
        RuleFor(t => t.EventId)
            .NotEmpty().WithMessage("Etkinlik ID'si boş olamaz.")
            .NotEqual(Guid.Empty).WithMessage("Geçerli bir etkinlik ID'si girilmelidir.");
    }
}
