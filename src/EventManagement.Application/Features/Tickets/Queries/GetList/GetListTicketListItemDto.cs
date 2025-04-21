using EventManagement.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Application.Features.Tickets.Queries.GetList;

/// <summary>
/// Represents a data transfer object (DTO) for listing tickets.
/// </summary>
public class GetListTicketListItemDto
{
    /// <summary>
    /// Gets or sets the unique identifier of the updated ticket.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the event associated with this ticket.
    /// </summary>
    public Guid EventId { get; set; }

    /// <summary>
    /// Gets or sets the type of the ticket (e.g., General, Student, VIP).
    /// </summary>
    public TicketType TicketType { get; set; }

    /// <summary>
    /// Gets or sets the price of the ticket.
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Gets or sets the number of available tickets for sale.
    /// </summary>
    public int QuantityAvailable { get; set; }

    /// <summary>
    /// Gets or sets the number of tickets that have been sold.
    /// </summary>
    public int QuantitySold { get; set; }
}
