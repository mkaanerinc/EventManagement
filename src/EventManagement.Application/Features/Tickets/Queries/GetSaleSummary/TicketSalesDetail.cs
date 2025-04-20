using EventManagement.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Application.Features.Tickets.Queries.GetSaleSummary;

/// <summary>
/// Represents the sales details for a specific ticket type.
/// </summary>
public class TicketSalesDetail
{
    /// <summary>
    /// Gets or sets the unique identifier of the ticket.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the type of the ticket (e.g., VIP, General, Student).
    /// </summary>
    public TicketType TicketType { get; set; }

    /// <summary>
    /// Gets or sets the price of the ticket.
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Gets or sets the quantity of tickets sold.
    /// </summary>
    public int QuantitySold { get; set; }

    /// <summary>
    /// Gets or sets the total revenue for this ticket type (Price * QuantitySold).
    /// </summary>
    public decimal Revenue { get; set; }
}