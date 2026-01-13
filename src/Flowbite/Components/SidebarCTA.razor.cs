using Flowbite.Utilities;
using Microsoft.AspNetCore.Components;

namespace Flowbite.Components;

/// <summary>
/// Call-to-action component for the Sidebar that can display buttons or other interactive elements.
/// </summary>
public partial class SidebarCTA
{
    /// <summary>
    /// Gets or sets the content to be rendered inside the CTA.
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// Gets or sets the color theme of the CTA.
    /// </summary>
    [Parameter]
    public SidebarCTAColor Color { get; set; } = SidebarCTAColor.Info;

    /// <summary>
    /// Gets or sets whether the CTA is collapsed. This is automatically handled by the parent Sidebar component.
    /// </summary>
    [CascadingParameter]
    private bool IsCollapsed { get; set; }

    private string GetClasses()
    {
        var colorClasses = Color switch
        {
            SidebarCTAColor.Blue => "bg-blue-50 text-blue-800 dark:bg-blue-900/50 dark:text-blue-400",
            SidebarCTAColor.Dark => "bg-gray-50 text-gray-800 dark:bg-gray-800/50 dark:text-gray-400",
            SidebarCTAColor.Failure => "bg-red-50 text-red-800 dark:bg-red-900/50 dark:text-red-400",
            SidebarCTAColor.Gray => "bg-gray-50 text-gray-800 dark:bg-gray-800/50 dark:text-gray-400",
            SidebarCTAColor.Green => "bg-green-50 text-green-800 dark:bg-green-900/50 dark:text-green-400",
            SidebarCTAColor.Info => "bg-blue-50 text-blue-800 dark:bg-blue-900/50 dark:text-blue-400",
            SidebarCTAColor.Light => "bg-gray-50 text-gray-800 dark:bg-gray-800/50 dark:text-gray-400",
            SidebarCTAColor.Purple => "bg-purple-50 text-purple-800 dark:bg-purple-900/50 dark:text-purple-400",
            SidebarCTAColor.Success => "bg-green-50 text-green-800 dark:bg-green-900/50 dark:text-green-400",
            SidebarCTAColor.Warning => "bg-yellow-50 text-yellow-800 dark:bg-yellow-900/50 dark:text-yellow-400",
            SidebarCTAColor.Yellow => "bg-yellow-50 text-yellow-800 dark:bg-yellow-900/50 dark:text-yellow-400",
            _ => string.Empty
        };

        return MergeClasses(
            ElementClass.Empty()
                .Add("mt-6 rounded-lg p-4")
                .Add(colorClasses)
                .Add(Class));
    }

    /// <summary>
    /// Defines the color theme for the SidebarCTA component.
    /// </summary>
    public enum SidebarCTAColor
    {
        /// <summary>
        /// Blue color theme
        /// </summary>
        Blue,
        
        /// <summary>
        /// Dark color theme
        /// </summary>
        Dark,
        
        /// <summary>
        /// Failure/Error color theme
        /// </summary>
        Failure,
        
        /// <summary>
        /// Gray color theme
        /// </summary>
        Gray,
        
        /// <summary>
        /// Green color theme
        /// </summary>
        Green,
        
        /// <summary>
        /// Info color theme (default)
        /// </summary>
        Info,
        
        /// <summary>
        /// Light color theme
        /// </summary>
        Light,
        
        /// <summary>
        /// Purple color theme
        /// </summary>
        Purple,
        
        /// <summary>
        /// Success color theme
        /// </summary>
        Success,
        
        /// <summary>
        /// Warning color theme
        /// </summary>
        Warning,
        
        /// <summary>
        /// Yellow color theme
        /// </summary>
        Yellow
    }
}
