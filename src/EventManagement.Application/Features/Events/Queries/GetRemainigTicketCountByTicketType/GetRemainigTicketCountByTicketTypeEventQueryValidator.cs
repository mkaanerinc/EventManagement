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
        RuleFor(e => e.EventId)
            .NotEmpty().WithMessage("Event ID cannot be empty.")
            .NotEqual(Guid.Empty).WithMessage("A valid event ID must be provided.");

        RuleFor(e => e.TicketType)
            .IsInEnum().WithMessage("A valid ticket type must be selected.");
    }
}
