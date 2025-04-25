using EventManagement.Domain.Enums;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Application.Features.Reports.Commands.Create;

/// <summary>
/// Validator class for <see cref="CreateReportCommand"/>.
/// Ensures that the input data is valid for creating a report.
/// </summary>
public class CreateReportCommandValidator : AbstractValidator<CreateReportCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateReportCommandValidator"/> class.
    /// Defines validation rules for each field of the <see cref="CreateReportCommand"/>.
    /// </summary>
    public CreateReportCommandValidator()
    {
        RuleFor(r => r.EventId)
            .NotEmpty().WithMessage("Event ID cannot be empty.")
            .NotEqual(Guid.Empty).WithMessage("A valid event ID must be provided.");

        RuleFor(r => r.ReportType)
            .IsInEnum().WithMessage("Report type must be a valid value.");

        RuleFor(r => r.ReportStatus)
            .IsInEnum().WithMessage("Status must be a valid value.");

        RuleFor(r => r.Result)
            .NotEmpty().When(r => r.ReportStatus == ReportStatus.Completed)
            .WithMessage("Result cannot be empty for a completed report.");

        RuleFor(r => r.CompletedAt)
            .NotEmpty().When(r => r.ReportStatus == ReportStatus.Completed && r.CompletedAt.HasValue)
            .WithMessage("Completed at must be specified.");
    }
}