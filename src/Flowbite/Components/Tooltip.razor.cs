using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Flowbite.Base;
using Flowbite.Services;

namespace Flowbite.Components;

/// <summary>
/// A tooltip component that displays informative text when hovering over, focusing on, or clicking an element.
/// </summary>
/// <remarks>
/// The Tooltip component provides contextual information in a small overlay.
/// It supports multiple trigger methods, placements, and visual styles.
/// Uses Floating UI for viewport-aware positioning with automatic flip and shift behavior.
/// </remarks>
/// <example>
/// <code>
/// &lt;Tooltip Content="Helpful information"&gt;
///     &lt;button&gt;Hover me&lt;/button&gt;
/// &lt;/Tooltip&gt;
/// </code>
/// </example>
public partial class Tooltip : FlowbiteComponentBase, IAsyncDisposable
{
    private bool _isVisible;
    private bool _isDisposed;
    private bool _isFocusLeaving;
    private bool _initialized;
    private string _id = $"tooltip-{Guid.NewGuid():N}"[..20];
    private string? _actualPlacement;

    [Inject]
    private IFloatingService FloatingService { get; set; } = default!;

    /// <summary>
    /// Handles mouse enter events on the tooltip trigger element.
    /// </summary>
    private async Task HandleMouseEnter()
    {
        if (Trigger == "hover")
        {
            _isVisible = true;
            _initialized = false;
            StateHasChanged();
            await Task.Yield();
            await InitializeFloatingAsync();
        }
    }

    /// <summary>
    /// Handles mouse leave events on the tooltip trigger element.
    /// </summary>
    private async Task HandleMouseLeave()
    {
        if (Trigger == "hover" && _isVisible)
        {
            await HideTooltipAsync();
        }
    }

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
            if (_isVisible)
            {
                await HideTooltipAsync();
            }
            else
            {
                _isVisible = true;
                _initialized = false;
                StateHasChanged();
                await Task.Yield();
                await InitializeFloatingAsync();
            }
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

        if (_isFocusLeaving && _isVisible)
        {
            await HideTooltipAsync();
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
            await HideTooltipAsync();
        }
    }

    private async Task HideTooltipAsync()
    {
        _isVisible = false;
        if (_initialized)
        {
            await FloatingService.DestroyAsync(_id);
            _initialized = false;
        }
    }

    private async Task InitializeFloatingAsync()
    {
        if (_initialized) return;

        var placement = GetFloatingPlacement();
        var options = new FloatingOptions(
            Placement: placement,
            Offset: 8,
            EnableFlip: true,
            EnableShift: true,
            ShiftPadding: 8
        );

        _actualPlacement = await FloatingService.InitializeAsync(_id, options);
        _initialized = true;
    }

    private string GetFloatingPlacement() => Placement switch
    {
        TooltipPlacement.Top => "top",
        TooltipPlacement.Bottom => "bottom",
        TooltipPlacement.Left => "left",
        TooltipPlacement.Right => "right",
        TooltipPlacement.Auto => "top", // Default to top for auto
        _ => "top"
    };

    /// <summary>
    /// Cleans up resources when the component is disposed.
    /// </summary>
    public async ValueTask DisposeAsync()
    {
        if (!_isDisposed)
        {
            _isDisposed = true;
            if (_initialized)
            {
                await FloatingService.DestroyAsync(_id);
            }
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
    /// The visual theme of the tooltip.
    /// </summary>
    /// <remarks>
    /// Supports "dark" (default) and "light" themes.
    /// Choose a theme that provides good contrast with the page background.
    /// </remarks>
    /// <example>
    /// <code>
    /// &lt;Tooltip Theme="light"&gt;
    ///     &lt;button&gt;Light theme&lt;/button&gt;
    /// &lt;/Tooltip&gt;
    /// </code>
    /// </example>
    [Parameter]
    public string Theme { get; set; } = "dark";

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

    private string GetTooltipClasses()
    {
        // Base classes - positioning is handled by Floating UI
        var baseClasses = "z-50 rounded-lg px-3 py-2 text-sm font-medium shadow-sm";

        var themeClasses = Theme switch
        {
            "light" => "border border-gray-200 bg-white text-gray-900",
            _ => "bg-gray-900 text-white dark:bg-gray-700"
        };

        var animationClasses = Animation != null
            ? $"transition-opacity motion-reduce:transition-none {Animation}"
            : "";

        return MergeClasses(baseClasses, themeClasses, animationClasses);
    }

    private string GetArrowClasses()
    {
        // Arrow positioning is handled by Floating UI's arrow middleware
        var baseClasses = "absolute h-2 w-2 rotate-45";

        var themeClasses = Theme switch
        {
            "light" => "bg-white",
            _ => "bg-gray-900 dark:bg-gray-700"
        };

        return MergeClasses(baseClasses, themeClasses);
    }
}
