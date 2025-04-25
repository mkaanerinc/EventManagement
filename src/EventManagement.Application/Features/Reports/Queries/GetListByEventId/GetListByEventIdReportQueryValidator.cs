using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Application.Features.Reports.Queries.GetListByEventId;

/// <summary>
/// Validator class for <see cref="GetListByEventIdReportQuery"/>.
/// Ensures that pagination parameters in <see cref="PageRequest"/> are within valid bounds.
/// Ensures the event ID is valid when retrieving a report by event ID.
/// </summary>
public class GetListByEventIdReportQueryValidator : AbstractValidator<GetListByEventIdReportQuery>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetListByEventIdReportQueryValidator"/> class.
    /// Defines validation rules for each field of the <see cref="GetListByEventIdReportQuery"/>.
    /// </summary>
    public GetListByEventIdReportQueryValidator()
    {
        RuleFor(r => r.EventId)
            .NotEmpty().WithMessage("Event ID cannot be empty.")
            .NotEqual(Guid.Empty).WithMessage("A valid event ID must be provided.");

        RuleFor(a => a.PageRequest.PageIndex)
             .GreaterThan(0).WithMessage("Page number must be greater than 0.");

        RuleFor(a => a.PageRequest.PageSize)
            .GreaterThan(0).WithMessage("Page size must be greater than 0.")
            .LessThanOrEqualTo(100).WithMessage("Page size can be at most 100.");
    }
}