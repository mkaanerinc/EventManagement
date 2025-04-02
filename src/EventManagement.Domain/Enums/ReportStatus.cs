using System;
using System.ComponentModel.DataAnnotations;

namespace EventManagement.Domain.Enums;

/// <summary>
/// Represents the status of a report generation process.
/// This enum uses the <see cref="DisplayAttribute"/> to provide localized names for each status.
/// The display names can be used in API responses or UI components for better readability.
/// </summary>
public enum ReportStatus
{
    /// <summary>
    /// The report is currently being generated.
    /// UI or API responses will show "Beklemede" instead of "Pending".
    /// </summary>
    [Display(Name = "Beklemede")]
    Pending = 1,

    /// <summary>
    /// The report has been successfully generated.
    /// UI or API responses will show "Tamamlandı" instead of "Completed".
    /// </summary>
    [Display(Name = "Tamamlandı")]
    Completed = 2,

    /// <summary>
    /// The report generation process has failed.
    /// UI or API responses will show "Başarısız" instead of "Failed".
    /// </summary>
    [Display(Name = "Başarısız")]
    Failed = 3
}
