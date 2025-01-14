using Flowbite.Components.Base;
using Microsoft.AspNetCore.Components.Web;

namespace Flowbite.Components;

/// <summary>
/// A tooltip component that displays informative text when hovering over, focusing on, or clicking an element.
/// </summary>
/// <remarks>
/// The Tooltip component provides contextual information in a small overlay.
/// It supports multiple trigger methods, placements, and visual styles.
/// </remarks>
/// <example>
/// <code>
/// &lt;Tooltip Content="Helpful information"&gt;
///     &lt;button&gt;Hover me&lt;/button&gt;
/// &lt;/Tooltip&gt;
/// </code>
/// </example>
public partial class Tooltip : IDisposable
{
    private bool _isVisible;
    private bool _isDisposed;
    private bool _isFocusLeaving;

    /// <summary>
    /// Handles click events on the tooltip trigger element.
    /// </summary>
    /// <remarks>
    /// Only toggles visibility when the trigger mode is set to "click".
    /// </remarks>
    private async Task HandleClick()
    {
        if (Trigger == "click")
        {
            _isVisible = !_isVisible;
            await Task.CompletedTask;
        }
    }

    /// <summary>
    /// Handles focus leaving the tooltip trigger element.
    /// </summary>
    /// <remarks>
    /// Includes a small delay to check if focus moved within the tooltip.
    /// Only applies when trigger mode is "click".
    /// </remarks>
    private async Task HandleFocusOut(FocusEventArgs args)
    {
        if (_isFocusLeaving || Trigger != "click")
        {
            return;
        }

        _isFocusLeaving = true;
        
        // Small delay to check if focus moved within tooltip
        await Task.Delay(10);
        
        if (_isFocusLeaving)
        {
            _isVisible = false;
        }
        
        _isFocusLeaving = false;
    }

    /// <summary>
    /// Handles keyboard events for accessibility.
    /// </summary>
    /// <remarks>
    /// Closes the tooltip when the Escape key is pressed.
    /// </remarks>
    private async Task HandleKeyDown(KeyboardEventArgs args)
    {
        if (args.Key == "Escape" && _isVisible)
        {
            _isVisible = false;
            await Task.CompletedTask;
        }
    }

    /// <summary>
    /// Cleans up resources when the component is disposed.
    /// </summary>
    public void Dispose()
    {
        if (!_isDisposed)
        {
            _isDisposed = true;
        }
    }

    /// <summary>
    /// Defines the placement options for the Tooltip component.
    /// </summary>
    public enum TooltipPlacement
    {
        /// <summary>
        /// Automatically choose the best placement
        /// </summary>
        Auto,

        /// <summary>
        /// Place the tooltip above the target element
        /// </summary>
        Top,

        /// <summary>
        /// Place the tooltip below the target element
        /// </summary>
        Bottom,

        /// <summary>
        /// Place the tooltip to the left of the target element
        /// </summary>
        Left,

        /// <summary>
        /// Place the tooltip to the right of the target element
        /// </summary>
        Right
    }

    /// <summary>
    /// The text content to display in the tooltip.
    /// </summary>
    /// <remarks>
    /// This is the main message that appears in the tooltip overlay.
    /// Keep the content concise for better user experience.
    /// </remarks>
    /// <example>
    /// <code>
    /// &lt;Tooltip Content="Click to save changes"&gt;
    ///     &lt;button&gt;Save&lt;/button&gt;
    /// &lt;/Tooltip&gt;
    /// </code>
    /// </example>
    [Parameter, EditorRequired]
    public string Content { get; set; } = string.Empty;

    /// <summary>
    /// Animation duration for showing/hiding the tooltip.
    /// </summary>
    /// <remarks>
    /// Controls the transition speed when the tooltip appears/disappears.
    /// Set to null to disable animation.
    /// </remarks>
    /// <example>
    /// <code>
    /// &lt;Tooltip Animation="duration-150"&gt;
    ///     &lt;button&gt;Quick animation&lt;/button&gt;
    /// &lt;/Tooltip&gt;
    /// </code>
    /// </example>
    [Parameter]
    public string? Animation { get; set; } = "duration-300";

    /// <summary>
    /// Whether to show an arrow pointing to the target element.
    /// </summary>
    /// <remarks>
    /// The arrow helps users understand which element the tooltip is referring to.
    /// </remarks>
    /// <example>
    /// <code>
    /// &lt;Tooltip Arrow="false"&gt;
    ///     &lt;button&gt;No arrow&lt;/button&gt;
    /// &lt;/Tooltip&gt;
    /// </code>
    /// </example>
    [Parameter]
    public bool Arrow { get; set; } = true;

    /// <summary>
    /// The placement of the tooltip relative to the target element.
    /// </summary>
    /// <remarks>
    /// Choose a placement that ensures the tooltip is visible within the viewport.
    /// The 'Auto' option will attempt to find the best placement automatically.
    /// </remarks>
    /// <example>
    /// <code>
    /// &lt;Tooltip Placement="TooltipPlacement.Right"&gt;
    ///     &lt;button&gt;Side tooltip&lt;/button&gt;
    /// &lt;/Tooltip&gt;
    /// </code>
    /// </example>
    [Parameter]
    public TooltipPlacement Placement { get; set; } = TooltipPlacement.Top;

    /// <summary>
    /// The visual style of the tooltip.
    /// </summary>
    /// <remarks>
    /// Supports "dark" (default) and "light" themes.
    /// Choose a style that provides good contrast with the page background.
    /// </remarks>
    /// <example>
    /// <code>
    /// &lt;Tooltip Style="light"&gt;
    ///     &lt;button&gt;Light theme&lt;/button&gt;
    /// &lt;/Tooltip&gt;
    /// </code>
    /// </example>
    [Parameter]
    public string Style { get; set; } = "dark";

    /// <summary>
    /// The event that triggers the tooltip.
    /// </summary>
    /// <remarks>
    /// Supports "hover" (default) and "click" modes.
    /// Use "click" for touch-friendly interfaces or complex tooltips.
    /// </remarks>
    /// <example>
    /// <code>
    /// &lt;Tooltip Trigger="click"&gt;
    ///     &lt;button&gt;Click me&lt;/button&gt;
    /// &lt;/Tooltip&gt;
    /// </code>
    /// </example>
    /// <accessibility>
    /// - "hover" mode requires keyboard focus support for accessibility
    /// - "click" mode should include keyboard interaction (Enter/Space to toggle)
    /// - Escape key should dismiss the tooltip when visible
    /// </accessibility>
    [Parameter]
    public string Trigger { get; set; } = "hover";

    /// <summary>
    /// The target element that the tooltip is attached to.
    /// </summary>
    /// <remarks>
    /// This is the element that triggers the tooltip when interacted with.
    /// Ensure the target element is focusable for keyboard accessibility.
    /// </remarks>
    /// <example>
    /// <code>
    /// &lt;Tooltip&gt;
    ///     &lt;button class="focus:ring-2"&gt;Accessible target&lt;/button&gt;
    /// &lt;/Tooltip&gt;
    /// </code>
    /// </example>
    /// <accessibility>
    /// The target element should:
    /// - Be focusable (button, link, or with tabindex)
    /// - Have appropriate ARIA attributes
    /// - Support keyboard interaction
    /// </accessibility>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    private string GetVisibilityClasses()
    {
        var hasAnimation = Animation != null;
        
        if (Trigger == "click")
        {
            if (!hasAnimation)
                return _isVisible ? "block" : "hidden";
                
            return _isVisible ? "opacity-100 block" : "opacity-0 hidden";
        }
        
        if (!hasAnimation)
            return "hidden group-hover:block";
            
        return "opacity-0 group-hover:opacity-100 group-hover:block";
    }

    private string GetPlacementClasses() => Placement switch
    {
        TooltipPlacement.Bottom => "top-full left-1/2 -translate-x-1/2 mt-2",
        TooltipPlacement.Left => "right-full top-1/2 -translate-y-1/2 mr-2",
        TooltipPlacement.Right => "left-full top-1/2 -translate-y-1/2 ml-2",
        _ => "bottom-full left-1/2 -translate-x-1/2 mb-2" // Top is default
    };

    private string GetStyleClasses() => Style switch
    {
        "light" => "inline-block rounded-lg px-3 py-2 text-sm font-medium shadow-sm border border-gray-200 bg-white text-gray-900",
        _ => "inline-block rounded-lg px-3 py-2 text-sm font-medium shadow-sm bg-gray-900 text-white dark:bg-gray-700"
    };

    private string GetAnimationClasses() => 
        Animation == null ? "" : $"transition-opacity {Animation}";

    private string GetArrowClasses() => Placement switch
    {
        TooltipPlacement.Bottom => "left-1/2 -ml-2 -top-1",
        TooltipPlacement.Left => "top-1/2 -m-1 right-0",
        TooltipPlacement.Right => "top-1/2 -m-1 left-0",
        _ => "left-1/2 -ml-4 -bottom-1"
    };

    private string GetArrowStyles() => Style switch
    {
        "light" => "bg-white",
        _ => "bg-gray-900 dark:bg-gray-700"
    };
}
