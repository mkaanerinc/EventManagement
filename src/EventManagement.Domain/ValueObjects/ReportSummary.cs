using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Domain.ValueObjects;

/// <summary>
/// Represents a summary of an event's financial and ticket sales data.
/// </summary>
public class ReportSummary
{
    /// <summary>
    /// Gets or sets the unique identifier of the event.
    /// </summary>
    public Guid EventId { get; set; }

    /// <summary>
    /// Gets or sets the total revenue generated from the event.
    /// </summary>
    public decimal TotalRevenue { get; set; }

    /// <summary>
    /// Gets or sets the total number of tickets sold for the event.
    /// </summary>
    public int TotalTicketsSold { get; set; }
}