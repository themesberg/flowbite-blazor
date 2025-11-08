namespace Flowbite.Components;

/// <summary>
/// Represents a single entry within a grouped timeline.
/// </summary>
public class GroupTimelineItem
{
    /// <summary>
    /// Raw HTML or text displayed as the entry title.
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Optional avatar image URL.
    /// </summary>
    public string? ImageSource { get; set; }

    /// <summary>
    /// Alt text for the avatar image.
    /// </summary>
    public string? ImageAlt { get; set; }

    /// <summary>
    /// Indicates whether the entry represents a private action.
    /// </summary>
    public bool IsPrivate { get; set; }

    /// <summary>
    /// Optional navigation target wrapping the entire item.
    /// </summary>
    public string? Href { get; set; }

    /// <summary>
    /// Optional comment displayed beneath the title.
    /// </summary>
    public string? Comment { get; set; }
}
