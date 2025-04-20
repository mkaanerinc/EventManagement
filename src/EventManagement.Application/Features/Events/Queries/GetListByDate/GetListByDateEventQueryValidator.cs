using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Application.Features.Events.Queries.GetListByDate;

/// <summary>
/// Validator class for <see cref="GetListByDateEventQuery"/>.
/// Ensures pagination and date parameters are valid for filtering events by date.
/// </summary>
public class GetListByDateEventQueryValidator : AbstractValidator<GetListByDateEventQuery>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetListByDateEventQueryValidator"/> class.
    /// Defines validation rules for pagination and date filtering.
    /// </summary>
    public GetListByDateEventQueryValidator()
    {
        RuleFor(e => e.PageRequest.PageIndex)
            .GreaterThan(0).WithMessage("Page number must be greater than 0.");

        RuleFor(e => e.PageRequest.PageSize)
            .GreaterThan(0).WithMessage("Page size must be greater than 0.")
            .LessThanOrEqualTo(100).WithMessage("Page size can be at most 100.");

        RuleFor(e => e.EventAt)
            .NotEmpty().WithMessage("Event date cannot be empty.")
            .Must(date => date.Date >= DateTimeOffset.MinValue.Date)
            .WithMessage("A valid event date must be provided.");
    }
}