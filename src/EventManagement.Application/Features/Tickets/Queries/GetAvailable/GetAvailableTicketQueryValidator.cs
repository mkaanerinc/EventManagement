using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Application.Features.Tickets.Queries.GetAvailable;

/// <summary>
/// Validator class for <see cref="GetAvailableTicketQuery"/>.
/// Ensures that pagination parameters in <see cref="PageRequest"/> are within valid bounds
/// and the event ID is valid when retrieving a ticket by event ID.
/// </summary>
public class GetAvailableTicketQueryValidator : AbstractValidator<GetAvailableTicketQuery>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetAvailableTicketQueryValidator"/> class.
    /// Defines validation rules for pagination parameters inside the query.
    /// </summary>
    public GetAvailableTicketQueryValidator()
    {
        RuleFor(t => t.PageRequest.PageIndex)
            .GreaterThan(0).WithMessage("Sayfa numarası 0'dan büyük olmalıdır.");

        RuleFor(t => t.PageRequest.PageSize)
            .GreaterThan(0).WithMessage("Sayfa boyutu 0'dan büyük olmalıdır.")
            .LessThanOrEqualTo(100).WithMessage("Sayfa boyutu en fazla 100 olabilir.");

        RuleFor(t => t.EventId)
            .NotEmpty().WithMessage("Etkinlik ID'si boş olamaz.")
            .NotEqual(Guid.Empty).WithMessage("Geçerli bir etkinlik ID'si girilmelidir.");
    }
}