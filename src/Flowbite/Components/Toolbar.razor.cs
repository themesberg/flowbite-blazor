using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Text;

namespace Flowbite.Components;

/// <summary>
/// Toolbar component for grouping actions and content with left/right layout.
/// Based on Flowbite Toolbar component pattern.
/// </summary>
public partial class Toolbar
{
    /// <summary>
    /// Main content area (left side of toolbar).
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// Optional content for the right side of the toolbar.
    /// </summary>
    [Parameter]
    public RenderFragment? End { get; set; }

    /// <summary>
    /// When true, removes background and border styling for embedded use.
    /// </summary>
    [Parameter]
    public bool Embedded { get; set; } = false;

    /// <summary>
    /// Additional attributes to be applied to the toolbar element.
    /// </summary>
    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object>? AdditionalAttributes { get; set; }

    private string GetToolbarClasses()
    {
        var classes = new StringBuilder();
        classes.Append("flex justify-between items-center");

        if (!Embedded)
        {
            classes.Append(" py-2 px-3 rounded-lg bg-gray-50 dark:bg-gray-800 dark:border dark:border-gray-600");
        }

        if (!string.IsNullOrWhiteSpace(Class))
        {
            classes.Append(" " + Class);
        }

        return classes.ToString();
    }

    private string GetContentClasses()
    {
        return "flex flex-wrap items-center";
    }
}
