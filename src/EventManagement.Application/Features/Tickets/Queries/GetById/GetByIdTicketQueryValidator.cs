using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Application.Features.Tickets.Queries.GetById;

/// <summary>
/// Validator class for <see cref="GetByIdTicketQuery"/>.
/// Ensures the ticket ID is valid when retrieving a ticket by ID.
/// </summary>
public class GetByIdTicketQueryValidator : AbstractValidator<GetByIdTicketQuery>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetByIdTicketQueryValidator"/> class.
    /// Defines validation rules for each field of the <see cref="GetByIdTicketQuery"/>.
    /// </summary>
    public GetByIdTicketQueryValidator()
    {
        RuleFor(t => t.Id)
            .NotEmpty().WithMessage("Bilet ID'si boş olamaz.")
            .NotEqual(Guid.Empty).WithMessage("Geçerli bir bilet ID'si girilmelidir.");
    }
}