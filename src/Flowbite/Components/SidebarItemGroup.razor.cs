namespace Flowbite.Components;

/// <summary>
/// Represents a group of sidebar items that can be organized together with optional separators.
/// </summary>
public partial class SidebarItemGroup
{
    /// <summary>
    /// Gets or sets additional CSS classes for the sidebar item group.
    /// </summary>
    [Parameter]
    public string? AdditionalClasses { get; set; }

    /// <summary>
    /// Gets or sets additional attributes for the sidebar item group.
    /// </summary>
    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object>? AdditionalAttributes { get; set; }
}
