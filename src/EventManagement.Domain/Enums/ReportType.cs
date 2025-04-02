using System;
using System.ComponentModel.DataAnnotations;

namespace EventManagement.Domain.Enums;

/// <summary>
/// Represents different types of reports that can be generated for an event.
/// This enum uses the <see cref="DisplayAttribute"/> to provide localized names for each status.
/// The display names can be used in API responses or UI components for better readability.
/// </summary>
public enum ReportType
{
    /// <summary>
    /// Report showing ticket sales data.
    /// UI or API responses will show "Bilet Satışları" instead of "TicketSales".
    /// </summary>
    [Display(Name = "Bilet Satışları")]
    TicketSales = 1,

    /// <summary>
    /// Report showing attendance statistics.
    /// UI or API responses will show "Katılımcı Listesi" instead of "Attendance".
    /// </summary>
    [Display(Name = "Katılımcı Listesi")]
    Attendance = 2
}
