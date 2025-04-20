using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Application.Features.Events.Queries.GetListOfUpcoming;

/// <summary>
/// Validator class for <see cref="GetListOfUpcomingEventQuery"/>.
/// Validates pagination properties such as page index and page size.
/// </summary>
public class GetListOfUpcomingEventQueryValidator : AbstractValidator<GetListOfUpcomingEventQuery>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetListOfUpcomingEventQueryValidator"/> class.
    /// Defines validation rules for pagination parameters.
    /// </summary>
    public GetListOfUpcomingEventQueryValidator()
    {
        RuleFor(e => e.PageRequest.PageIndex)
            .GreaterThan(0).WithMessage("Page number must be greater than 0.");

        RuleFor(e => e.PageRequest.PageSize)
            .GreaterThan(0).WithMessage("Page size must be greater than 0.")
            .LessThanOrEqualTo(100).WithMessage("Page size can be at most 100.");
    }
}
