using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Application.Features.Tickets.Commands.Delete;

/// <summary>
/// Represents the response returned after successfully deleting a ticket.
/// </summary>
public class DeletedTicketResponse
{
    /// <summary>
    /// Gets or sets the unique identifier of the deleted ticket.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the timestamp of when the ticket was deleted.
    /// </summary>
    public DateTimeOffset DeletedAt { get; set; }
}
