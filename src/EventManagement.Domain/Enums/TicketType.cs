using System;
using System.ComponentModel.DataAnnotations;

namespace EventManagement.Domain.Enums;

/// <summary>
/// Represents the type of ticket available for an event.
/// This enum uses the <see cref="DisplayAttribute"/> to provide localized names for each status.
/// The display names can be used in API responses or UI components for better readability.
/// </summary>
public enum TicketType
{
    /// <summary>
    /// Standard ticket type for general attendees.
    /// UI or API responses will show "Genel" instead of "General".
    /// </summary>
    [Display(Name = "Genel")]
    General = 1,

    /// <summary>
    /// Discounted ticket type for students.
    /// UI or API responses will show "Öğrenci" instead of "Student".
    /// </summary>
    [Display(Name = "Öğrenci")]
    Student = 2,

    /// <summary>
    /// Premium ticket type with additional privileges.
    /// </summary>
    VIP = 3
}
