namespace Flowbite.Common;

/// <summary>
/// Represents the animation state of a collapsible element.
/// Used for managing smooth height transitions in components like SidebarCollapse.
/// </summary>
public enum CollapseState
{
    /// <summary>
    /// Height = 0, content hidden. The element is fully collapsed.
    /// </summary>
    Collapsed,

    /// <summary>
    /// Height animating from 0 to scrollHeight. The element is expanding.
    /// </summary>
    Expanding,

    /// <summary>
    /// Height = auto, content visible. The element is fully expanded.
    /// </summary>
    Expanded,

    /// <summary>
    /// Height animating from scrollHeight to 0. The element is collapsing.
    /// </summary>
    Collapsing
}
