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
            .GreaterThan(0).WithMessage("Sayfa numarası 0'dan büyük olmalıdır.");

        RuleFor(e => e.PageRequest.PageSize)
            .GreaterThan(0).WithMessage("Sayfa boyutu 0'dan büyük olmalıdır.")
            .LessThanOrEqualTo(100).WithMessage("Sayfa boyutu en fazla 100 olabilir.");

        RuleFor(e => e.StartAt)
            .NotEmpty().WithMessage("Başlangıç tarihi boş olamaz.");

        RuleFor(e => e.EndAt)
            .NotEmpty().WithMessage("Bitiş tarihi boş olamaz.");

        RuleFor(e => e)
            .Must(e => e.StartAt <= e.EndAt)
            .WithMessage("Başlangıç tarihi, bitiş tarihinden sonra olamaz.");
    }
}
