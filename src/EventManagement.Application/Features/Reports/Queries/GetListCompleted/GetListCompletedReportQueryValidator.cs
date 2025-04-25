using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Application.Features.Reports.Queries.GetListCompleted;

/// <summary>
/// Validator class for <see cref="GetListCompletedReportQuery"/>.
/// Ensures that pagination parameters in <see cref="PageRequest"/> are within valid bounds.
/// </summary>
public class GetListCompletedReportQueryValidator : AbstractValidator<GetListCompletedReportQuery>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetListCompletedReportQueryValidator"/> class.
    /// Defines validation rules for pagination parameters inside the query.
    /// </summary>
    public GetListCompletedReportQueryValidator()
    {
        RuleFor(r => r.PageRequest.PageIndex)
            .GreaterThan(0).WithMessage("Page number must be greater than 0.");

        RuleFor(r => r.PageRequest.PageSize)
            .GreaterThan(0).WithMessage("Page size must be greater than 0.")
            .LessThanOrEqualTo(100).WithMessage("Page size can be at most 100.");
    }
}