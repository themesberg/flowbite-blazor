using Microsoft.AspNetCore.Components;
using Flowbite.Base;

namespace Flowbite.Components;

/// <summary>
/// A spinner component for indicating loading states and ongoing processes.
/// </summary>
/// <remarks>
/// The Spinner component provides visual feedback for loading states in your application.
/// It supports various sizes and colors to match your design system, and includes
/// built-in dark mode support.
/// </remarks>
/// <example>
/// <code>
/// &lt;Spinner Size="SpinnerSize.Lg" Color="SpinnerColor.Info" /&gt;
/// </code>
/// </example>
/// <accessibility>
/// The spinner is automatically assigned appropriate ARIA attributes for accessibility:
/// - aria-busy="true" indicates a loading state
/// - role="status" identifies it as a status indicator
/// - aria-label describes the loading state for screen readers
/// </accessibility>
public partial class Spinner : FlowbiteComponentBase
{
    private const string BaseClasses = "inline animate-spin text-gray-200 dark:text-gray-600";

    /// <summary>
    /// The color variant of the spinner.
    /// </summary>
    /// <remarks>
    /// Choose a color that provides sufficient contrast with the background and
    /// matches the context of the loading state (e.g., Success for positive operations).
    /// </remarks>
    /// <example>
    /// <code>
    /// // Success spinner for positive feedback
    /// &lt;Spinner Color="SpinnerColor.Success" /&gt;
    /// 
    /// // Warning spinner for processes that need attention
    /// &lt;Spinner Color="SpinnerColor.Warning" /&gt;
    /// </code>
    /// </example>
    [Parameter]
    public SpinnerColor Color { get; set; } = SpinnerColor.Info;

    /// <summary>
    /// The size variant of the spinner.
    /// </summary>
    /// <remarks>
    /// Select a size appropriate for your use case:
    /// - Xs/Sm: Inline with text or in tight spaces
    /// - Md: Default size, suitable for most cases
    /// - Lg/Xl: When the spinner needs to be more prominent
    /// </remarks>
    /// <example>
    /// <code>
    /// // Small spinner inline with text
    /// &lt;p&gt;Loading &lt;Spinner Size="SpinnerSize.Sm" /&gt;&lt;/p&gt;
    /// 
    /// // Large spinner for page-level loading
    /// &lt;div class="flex justify-center"&gt;
    ///     &lt;Spinner Size="SpinnerSize.Xl" /&gt;
    /// &lt;/div&gt;
    /// </code>
    /// </example>
    [Parameter]
    public SpinnerSize Size { get; set; } = SpinnerSize.Md;

    /// <summary>
    /// Additional attributes that will be merged with the component's native attributes.
    /// </summary>
    /// <remarks>
    /// Useful for adding custom data attributes, event handlers, or CSS classes.
    /// These attributes will be merged with the spinner's base attributes.
    /// </remarks>
    /// <example>
    /// <code>
    /// &lt;Spinner @attributes="new Dictionary&lt;string, object&gt;
    /// {
    ///     { "data-testid", "loading-spinner" },
    ///     { "class", "my-custom-class" }
    /// }" /&gt;
    /// </code>
    /// </example>
    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object>? AdditionalAttributes { get; set; }

    /// <summary>
    /// Gets the CSS classes for the spinner.
    /// </summary>
    /// <returns>A string containing all the CSS classes for the spinner.</returns>
    private string GetSpinnerClasses()
    {
        var classes = new List<string>
        {
            BaseClasses,
            GetSizeClasses(),
            GetColorClasses()
        };

        return string.Join(" ", classes);
    }

    private string GetSizeClasses() => Size switch
    {
        SpinnerSize.Xs => "h-3 w-3",
        SpinnerSize.Sm => "h-4 w-4",
        SpinnerSize.Md => "h-6 w-6",
        SpinnerSize.Lg => "h-8 w-8",
        SpinnerSize.Xl => "h-10 w-10",
        _ => "h-6 w-6"
    };

    private string GetColorClasses() => Color switch
    {
        SpinnerColor.Info => "fill-primary-600 dark:fill-primary-400",
        SpinnerColor.Success => "fill-green-500 dark:fill-green-400",
        SpinnerColor.Warning => "fill-yellow-400 dark:fill-yellow-300",
        SpinnerColor.Failure => "fill-red-600 dark:fill-red-500",
        SpinnerColor.Pink => "fill-pink-600 dark:fill-pink-500",
        SpinnerColor.Purple => "fill-purple-600 dark:fill-purple-500",
        SpinnerColor.Gray => "fill-gray-600 dark:fill-gray-500",
        _ => "fill-primary-600 dark:fill-primary-500"
    };
}

/// <summary>
/// Available color variants for the spinner.
/// </summary>
/// <remarks>
/// Each color variant is designed to convey different states or contexts:
/// - Info: Default color, suitable for general loading states
/// - Success/Failure: Indicate the status of an operation
/// - Warning: Draw attention to important processes
/// - Others: Additional options for matching your design system
/// </remarks>
public enum SpinnerColor
{
    /// <summary>
    /// Primary color variant, suitable for general loading states
    /// </summary>
    Info,

    /// <summary>
    /// Green color variant, indicates successful or positive operations
    /// </summary>
    Success,

    /// <summary>
    /// Yellow color variant, indicates operations needing attention
    /// </summary>
    Warning,

    /// <summary>
    /// Red color variant, indicates failed or negative operations
    /// </summary>
    Failure,

    /// <summary>
    /// Pink color variant, alternative accent color
    /// </summary>
    Pink,

    /// <summary>
    /// Purple color variant, alternative accent color
    /// </summary>
    Purple,

    /// <summary>
    /// Gray color variant, suitable for subtle loading indicators
    /// </summary>
    Gray
}

/// <summary>
/// Available size variants for the spinner.
/// </summary>
/// <remarks>
/// Size variants allow the spinner to be used in different contexts:
/// - Xs/Sm: Inline with text or in compact UIs
/// - Md: Standard size for most use cases
/// - Lg/Xl: Large, prominent loading indicators
/// </remarks>
public enum SpinnerSize
{
    /// <summary>
    /// Extra small size (12x12px), ideal for inline text
    /// </summary>
    Xs,

    /// <summary>
    /// Small size (16x16px), suitable for compact spaces
    /// </summary>
    Sm,

    /// <summary>
    /// Medium size (24x24px), default size for most cases
    /// </summary>
    Md,

    /// <summary>
    /// Large size (32x32px), for prominent loading states
    /// </summary>
    Lg,

    /// <summary>
    /// Extra large size (40x40px), for major loading indicators
    /// </summary>
    Xl
}
