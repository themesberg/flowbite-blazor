namespace Flowbite.Components;

/// <summary>
/// Determines the layout variant for the timeline component.
/// </summary>
public enum TimelineOrder
{
    /// <summary>
    /// Default vertical layout with inline dates.
    /// </summary>
    Default,

    /// <summary>
    /// Vertical layout with timeline indicators and icons.
    /// </summary>
    Vertical,

    /// <summary>
    /// Horizontal layout for representing chronological steps.
    /// </summary>
    Horizontal,

    /// <summary>
    /// Activity layout used alongside <see cref="Activity"/> components.
    /// </summary>
    Activity
}

/// <summary>
/// Color variants for timeline items.
/// </summary>
public enum TimelineColor
{
    Primary,
    Green,
    Orange,
    Red,
    Blue,
    Purple,
    Gray
}

/// <summary>
/// Supported formats when displaying timeline item dates.
/// </summary>
public enum TimelineDateFormat
{
    /// <summary>
    /// Displays the month and year (e.g. January 2024).
    /// </summary>
    MonthYear,

    /// <summary>
    /// Displays the full year (e.g. 2024).
    /// </summary>
    Year,

    /// <summary>
    /// Displays the day, month, and year (e.g. 12 January 2024).
    /// </summary>
    FullDate
}
