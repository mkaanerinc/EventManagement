using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Application.Features.Events.Queries.GetListByDateRange;

/// <summary>
/// Validator class for <see cref="GetListByDateRangeEventQuery"/>.
/// Ensures that pagination parameters and date range are valid for retrieving events.
/// </summary>
public class GetListByDateRangeEventQueryValidator : AbstractValidator<GetListByDateRangeEventQuery>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetListByDateRangeEventQueryValidator"/> class.
    /// Defines validation rules for pagination and date range fields.
    /// </summary>
    public GetListByDateRangeEventQueryValidator()
    {
        RuleFor(e => e.PageRequest.PageIndex)
            .GreaterThan(0).WithMessage("Page number must be greater than 0.");

        RuleFor(e => e.PageRequest.PageSize)
            .GreaterThan(0).WithMessage("Page size must be greater than 0.")
            .LessThanOrEqualTo(100).WithMessage("Page size can be at most 100.");

        RuleFor(e => e.StartAt)
            .NotEmpty().WithMessage("Start date cannot be empty.");

        RuleFor(e => e.EndAt)
            .NotEmpty().WithMessage("End date cannot be empty.");

        RuleFor(e => e)
            .Must(e => e.StartAt <= e.EndAt)
            .WithMessage("Start date cannot be after the end date.");
    }
}
