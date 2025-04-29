using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Application.Features.Reports.Queries.GetSummaryByEventId;

/// <summary>
/// Validator class for <see cref="GetSummaryByEventIdReportQuery"/>.
/// Ensures the event ID is valid when retrieving a report summary by event ID.
/// </summary>
public class GetSummaryByEventIdReportQueryValidator : AbstractValidator<GetSummaryByEventIdReportQuery>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetSummaryByEventIdReportQueryValidator"/> class.
    /// Defines validation rules for each field of the <see cref="GetSummaryByEventIdReportQuery"/>.
    /// </summary>
    public GetSummaryByEventIdReportQueryValidator()
    {
        RuleFor(r => r.EventId)
            .NotEmpty().WithMessage("Event ID cannot be empty.")
            .NotEqual(Guid.Empty).WithMessage("A valid event ID must be provided.");
    }
}