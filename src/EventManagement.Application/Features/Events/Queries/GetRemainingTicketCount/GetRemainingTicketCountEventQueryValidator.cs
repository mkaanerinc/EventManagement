using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Application.Features.Events.Queries.GetRemainingTicketCount;

/// <summary>
/// Validator class for <see cref="GetRemainingTicketCountEventQuery"/>.
/// Ensures the event ID is valid for retrieving the remaining ticket count.
/// </summary>
public class GetRemainingTicketCountEventQueryValidator : AbstractValidator<GetRemainingTicketCountEventQuery>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetRemainingTicketCountEventQueryValidator"/> class.
    /// Defines validation rules for the EventId property.
    /// </summary>
    public GetRemainingTicketCountEventQueryValidator()
    {
        RuleFor(e => e.EventId)
            .NotEmpty().WithMessage("Event ID cannot be empty.")
            .NotEqual(Guid.Empty).WithMessage("A valid event ID must be provided.");
    }
}
