using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Application.Features.Reports.Queries.GetById;

/// <summary>
/// Validator class for <see cref="GetByIdReportQuery"/>.
/// Ensures the report ID is valid when retrieving a report by ID.
/// </summary>
public class GetByIdReportQueryValidator : AbstractValidator<GetByIdReportQuery>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetByIdReportQueryValidator"/> class.
    /// Defines validation rules for each field of the <see cref="GetByIdReportQuery"/>.
    /// </summary>
    public GetByIdReportQueryValidator()
    {
        RuleFor(t => t.Id)
            .NotEmpty().WithMessage("Report ID cannot be empty.")
            .NotEqual(Guid.Empty).WithMessage("A valid report ID must be provided.");
    }
}