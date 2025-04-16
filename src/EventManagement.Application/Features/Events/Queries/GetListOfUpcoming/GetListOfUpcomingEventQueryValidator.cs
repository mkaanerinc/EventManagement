using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Application.Features.Events.Queries.GetListOfUpcoming;

/// <summary>
/// Validator class for <see cref="GetListOfUpcomingEventQuery"/>.
/// Validates pagination properties such as page index and page size.
/// </summary>
public class GetListOfUpcomingEventQueryValidator : AbstractValidator<GetListOfUpcomingEventQuery>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetListOfUpcomingEventQueryValidator"/> class.
    /// Defines validation rules for pagination parameters.
    /// </summary>
    public GetListOfUpcomingEventQueryValidator()
    {
        RuleFor(q => q.PageRequest.PageIndex)
            .GreaterThan(0).WithMessage("Sayfa numarası 0'dan büyük olmalıdır.");

        RuleFor(q => q.PageRequest.PageSize)
            .GreaterThan(0).WithMessage("Sayfa boyutu 0'dan büyük olmalıdır.")
            .LessThanOrEqualTo(100).WithMessage("Sayfa boyutu en fazla 100 olabilir.");
    }
}
