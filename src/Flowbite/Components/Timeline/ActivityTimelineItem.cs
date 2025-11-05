namespace Flowbite.Components;

/// <summary>
/// Represents a single entry displayed by the <see cref="ActivityItem"/> component.
/// </summary>
public class ActivityTimelineItem
{
    /// <summary>
    /// Raw HTML or text describing the activity performer and action.
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Short description of when the activity occurred.
    /// </summary>
    public string Date { get; set; } = string.Empty;

    /// <summary>
    /// Image source used for the entry avatar.
    /// </summary>
    public string? ImageSource { get; set; }

    /// <summary>
    /// Alt text applied to the avatar image.
    /// </summary>
    public string? ImageAlt { get; set; }

    /// <summary>
    /// Additional message rendered beneath the header. Supports raw HTML.
    /// </summary>
    public string? Text { get; set; }
}
