using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Application.Features.Attendees.Queries.GetList;

/// <summary>
/// Validator class for <see cref="GetListAttendeeQuery"/>.
/// Ensures that pagination parameters in <see cref="PageRequest"/> are within valid bounds.
/// </summary>
public class GetListAttendeeQueryValidator : AbstractValidator<GetListAttendeeQuery>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetListAttendeeQueryValidator"/> class.
    /// Defines validation rules for pagination parameters inside the query.
    /// </summary>
    public GetListAttendeeQueryValidator()
    {
        RuleFor(a => a.PageRequest.PageIndex)
             .GreaterThan(0).WithMessage("Page number must be greater than 0.");

        RuleFor(a => a.PageRequest.PageSize)
            .GreaterThan(0).WithMessage("Page size must be greater than 0.")
            .LessThanOrEqualTo(100).WithMessage("Page size can be at most 100.");
    }
}