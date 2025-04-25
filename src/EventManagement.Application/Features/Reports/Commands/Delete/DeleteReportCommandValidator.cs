using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Application.Features.Reports.Commands.Delete;

/// <summary>
/// Validator class for <see cref="DeleteReportCommand"/>.
/// Ensures the report ID is valid for deleting a report.
/// </summary>
public class DeleteReportCommandValidator : AbstractValidator<DeleteReportCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteReportCommandValidator"/> class.
    /// Defines validation rules for each field of the <see cref="DeleteReportCommand"/>.
    /// </summary>
    public DeleteReportCommandValidator()
    {
        RuleFor(r => r.Id)
            .NotEmpty().WithMessage("Report ID cannot be empty.")
            .NotEqual(Guid.Empty).WithMessage("A valid report ID must be provided.");
    }
}