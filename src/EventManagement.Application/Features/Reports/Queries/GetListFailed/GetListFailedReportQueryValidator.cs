using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Application.Features.Reports.Queries.GetListFailed;

/// <summary>
/// Validator class for <see cref="GetListFailedReportQuery"/>.
/// Ensures that pagination parameters in <see cref="PageRequest"/> are within valid bounds.
/// </summary>
public class GetListFailedReportQueryValidator : AbstractValidator<GetListFailedReportQuery>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetListFailedReportQueryValidator"/> class.
    /// Defines validation rules for pagination parameters inside the query.
    /// </summary>
    public GetListFailedReportQueryValidator()
    {
        RuleFor(r => r.PageRequest.PageIndex)
            .GreaterThan(0).WithMessage("Page number must be greater than 0.");

        RuleFor(r => r.PageRequest.PageSize)
            .GreaterThan(0).WithMessage("Page size must be greater than 0.")
            .LessThanOrEqualTo(100).WithMessage("Page size can be at most 100.");
    }
}