using Flowbite.Components.Base;

namespace Flowbite.Components;

public partial class SidebarItem
{
    /// <summary>
    /// Defines the color options for the label
    /// </summary>
    public enum LabelColor
    {
        /// <summary>
        /// Default gray color
        /// </summary>
        Default,

        /// <summary>
        /// Primary blue color
        /// </summary>
        Primary,

        /// <summary>
        /// Success green color
        /// </summary>
        Success,

        /// <summary>
        /// Warning yellow color
        /// </summary>
        Warning,

        /// <summary>
        /// Danger red color
        /// </summary>
        Danger
    }

    /// <summary>
    /// The URL that the sidebar item links to
    /// </summary>
    [Parameter]
    public string? Href { get; set; }

    /// <summary>
    /// Optional icon to display next to the item text.
    /// The icon will inherit the item's color scheme.
    /// </summary>
    [Parameter]
    public IconBase? Icon { get; set; }

    /// <summary>
    /// Optional label text to display (e.g. "New" or "5 unread")
    /// </summary>
    [Parameter]
    public string? Label { get; set; }

    /// <summary>
    /// The color of the label
    /// </summary>
    [Parameter]
    public LabelColor LabelColorValue { get; set; } = LabelColor.Default;

    /// <summary>
    /// The content to display as the item text
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    private string GetClasses()
    {
        return string.Empty; // Base div doesn't need additional classes
    }

    private string GetLabelClasses()
    {
        var baseClasses = "inline-flex items-center justify-center px-2 text-sm font-medium";

        var colorClasses = LabelColorValue switch
        {
            LabelColor.Primary => "text-blue-800 bg-blue-100 rounded-full dark:bg-blue-900 dark:text-blue-300",
            LabelColor.Success => "text-green-800 bg-green-100 rounded-full dark:bg-green-900 dark:text-green-300",
            LabelColor.Warning => "text-yellow-800 bg-yellow-100 rounded-full dark:bg-yellow-900 dark:text-yellow-300",
            LabelColor.Danger => "text-red-800 bg-red-100 rounded-full dark:bg-red-900 dark:text-red-300",
            _ => "text-gray-800 bg-gray-100 rounded-full dark:bg-gray-900 dark:text-gray-300"
        };

        return $"{baseClasses} {colorClasses}";
    }
}
