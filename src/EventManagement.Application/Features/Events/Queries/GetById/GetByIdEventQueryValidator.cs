using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Application.Features.Events.Queries.GetById;

/// <summary>
/// Validator class for <see cref="GetByIdEventQuery"/>.
/// Ensures the event ID is valid when retrieving an event by ID.
/// </summary>
public class GetByIdEventQueryValidator : AbstractValidator<GetByIdEventQuery>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetByIdEventQueryValidator"/> class.
    /// Defines validation rules for each field of the <see cref="GetByIdEventQuery"/>.
    /// </summary>
    public GetByIdEventQueryValidator()
    {
        RuleFor(e => e.Id)
            .NotEmpty().WithMessage("Event ID cannot be empty.")
            .NotEqual(Guid.Empty).WithMessage("A valid event ID must be provided.");
    }
}
