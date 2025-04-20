using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Application.Features.Tickets.Queries.GetSaleSummary;

/// <summary>
/// Represents the response data for ticket sales summary.
/// </summary>
public class GetSaleSummaryTicketResponse
{
    /// <summary>
    /// Gets or sets the unique identifier of the event.
    /// </summary>
    public Guid EventId { get; set; }

    /// <summary>
    /// Gets or sets the total revenue from ticket sales (Price * QuantitySold).
    /// </summary>
    public decimal TotalRevenue { get; set; }

    /// <summary>
    /// Gets or sets the total number of tickets sold.
    /// </summary>
    public int TotalTicketsSold { get; set; }

    /// <summary>
    /// Gets or sets the list of ticket sales details for each ticket type.
    /// </summary>
    public required List<TicketSalesDetail> TicketSalesDetails { get; set; }
}