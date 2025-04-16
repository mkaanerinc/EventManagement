using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Application.Features.Events.Queries.GetList;

/// <summary>
/// Validator class for <see cref="GetListEventQuery"/>.
/// Ensures that pagination parameters in <see cref="PageRequest"/> are within valid bounds.
/// </summary>
public class GetListEventQueryValidator : AbstractValidator<GetListEventQuery>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetListEventQueryValidator"/> class.
    /// Defines validation rules for pagination parameters inside the query.
    /// </summary>
    public GetListEventQueryValidator()
    {
        RuleFor(e => e.PageRequest.PageIndex)
            .GreaterThan(0).WithMessage("Sayfa numarası 0'dan büyük olmalıdır.");

        RuleFor(e => e.PageRequest.PageSize)
            .GreaterThan(0).WithMessage("Sayfa boyutu 0'dan büyük olmalıdır.")
            .LessThanOrEqualTo(100).WithMessage("Sayfa boyutu en fazla 100 olabilir.");
    }
}