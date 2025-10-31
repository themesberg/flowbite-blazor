using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Collections.Generic;
using System.Text;

namespace Flowbite.Components;

/// <summary>
/// Toolbar button component for icon-based actions.
/// Based on Flowbite ToolbarButton component pattern.
/// </summary>
public partial class ToolbarButton
{
    /// <summary>
    /// Content to display inside the button (typically an icon).
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// Event callback when the button is clicked.
    /// </summary>
    [Parameter]
    public EventCallback<MouseEventArgs> OnClick { get; set; }

    /// <summary>
    /// Color variant for the button.
    /// </summary>
    [Parameter]
    public ToolbarButtonColor Color { get; set; } = ToolbarButtonColor.Gray;

    /// <summary>
    /// Size variant for the button.
    /// </summary>
    [Parameter]
    public ToolbarButtonSize Size { get; set; } = ToolbarButtonSize.Medium;

    /// <summary>
    /// Accessibility label for screen readers.
    /// </summary>
    [Parameter]
    public string? AriaLabel { get; set; }

    /// <summary>
    /// Additional attributes to be applied to the button element.
    /// </summary>
    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object>? AdditionalAttributes { get; set; }

    private async Task HandleClick(MouseEventArgs e)
    {
        await OnClick.InvokeAsync(e);
    }

    private string GetButtonClasses()
    {
        var classes = new StringBuilder();
        classes.Append("focus:outline-hidden whitespace-normal");

        // Color classes
        classes.Append(Color switch
        {
            ToolbarButtonColor.Dark => " text-gray-400 hover:text-gray-700 hover:bg-gray-200 dark:text-gray-500 dark:hover:text-gray-300 dark:hover:bg-gray-600",
            ToolbarButtonColor.Gray => " text-gray-400 hover:text-gray-700 hover:bg-gray-200 dark:text-gray-500 dark:hover:text-gray-300 dark:hover:bg-gray-600",
            _ => " text-gray-400 hover:text-gray-700 hover:bg-gray-200 dark:text-gray-500 dark:hover:text-gray-300 dark:hover:bg-gray-600"
        });

        // Size classes
        classes.Append(Size switch
        {
            ToolbarButtonSize.ExtraSmall => " m-0.5 rounded-xs focus:ring-1 p-0.5",
            ToolbarButtonSize.Small => " m-0.5 rounded-sm focus:ring-1 p-0.5",
            ToolbarButtonSize.Medium => " m-0.5 rounded-lg focus:ring-2 p-1.5",
            ToolbarButtonSize.Large => " m-0.5 rounded-lg focus:ring-2 p-2.5",
            _ => " m-0.5 rounded-lg focus:ring-2 p-1.5"
        });

        if (!string.IsNullOrWhiteSpace(Class))
        {
            classes.Append(" " + Class);
        }

        return classes.ToString();
    }
}

/// <summary>
/// Color variants for ToolbarButton component.
/// </summary>
public enum ToolbarButtonColor
{
    Gray,
    Dark
}

/// <summary>
/// Size variants for ToolbarButton component.
/// </summary>
public enum ToolbarButtonSize
{
    ExtraSmall,
    Small,
    Medium,
    Large
}
