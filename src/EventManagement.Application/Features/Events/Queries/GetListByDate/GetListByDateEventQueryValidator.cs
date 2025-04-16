using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Application.Features.Events.Queries.GetListByDate;

/// <summary>
/// Validator class for <see cref="GetListByDateEventQuery"/>.
/// Ensures pagination and date parameters are valid for filtering events by date.
/// </summary>
public class GetListByDateEventQueryValidator : AbstractValidator<GetListByDateEventQuery>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetListByDateEventQueryValidator"/> class.
    /// Defines validation rules for pagination and date filtering.
    /// </summary>
    public GetListByDateEventQueryValidator()
    {
        RuleFor(e => e.PageRequest.PageIndex)
            .GreaterThan(0).WithMessage("Sayfa numarası 0'dan büyük olmalıdır.");

        RuleFor(e => e.PageRequest.PageSize)
            .GreaterThan(0).WithMessage("Sayfa boyutu 0'dan büyük olmalıdır.")
            .LessThanOrEqualTo(100).WithMessage("Sayfa boyutu en fazla 100 olabilir.");

        RuleFor(e => e.EventAt)
            .NotEmpty().WithMessage("Etkinlik tarihi boş olamaz.")
            .Must(date => date.Date >= DateTimeOffset.MinValue.Date)
            .WithMessage("Geçerli bir etkinlik tarihi girilmelidir.");
    }
}