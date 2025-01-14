using Microsoft.AspNetCore.Components;

namespace Flowbite.Components;

/// <summary>
/// Context class for sharing tab state between tab components.
/// </summary>
public class TabContext
{
    /// <summary>
    /// The currently active tab index.
    /// </summary>
    public int ActiveIndex { get; set; }

    /// <summary>
    /// Event callback for when the active tab changes.
    /// </summary>
    public EventCallback<int> ActiveIndexChanged { get; set; }

    /// <summary>
    /// The visual style variant of the tabs.
    /// </summary>
    public TabVariant Variant { get; set; }

    /// <summary>
    /// Changes the active tab index and invokes the change callback.
    /// </summary>
    /// <param name="index">The new active tab index</param>
    public async Task SetActiveIndexAsync(int index)
    {
        if (ActiveIndex != index)
        {
            ActiveIndex = index;
            await ActiveIndexChanged.InvokeAsync(index);
        }
    }
}

/// <summary>
/// Defines the visual style variants available for the Tabs component.
/// </summary>
public enum TabVariant
{
    /// <summary>
    /// Default tab style with bottom border.
    /// </summary>
    Default,

    /// <summary>
    /// Full width tabs that span the container.
    /// </summary>
    FullWidth,

    /// <summary>
    /// Pill-shaped tabs with rounded corners.
    /// </summary>
    Pills,

    /// <summary>
    /// Underlined tabs with bottom border indicator.
    /// </summary>
    Underline
}
