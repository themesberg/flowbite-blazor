using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;


namespace Flowbite.Components;

/// <summary>
/// Alert component for displaying contextual feedback messages with various color schemes and configurations.
/// </summary>
/// <remarks>
/// Provides a flexible way to display informative, warning, success, and error messages with customizable appearance.
/// Supports icons, dismissal, and different color variants for different contexts.
/// </remarks>
public partial class Alert
{
    private string BaseClasses => "flex flex-col gap-2 p-4 text-sm";
    private string RoundedClasses => "rounded-lg";
    private string BorderAccentClasses => "border-t-4";

    /// <summary>
    /// The main text content of the alert.
    /// </summary>
    /// <remarks>
    /// Provides the primary message to be displayed in the alert.
    /// </remarks>
    /// <example>
    /// <code>
    /// &lt;Alert Text="This is an important message" /&gt;
    /// </code>
    /// </example>
    [Parameter]
    public string? Text { get; set; }

    /// <summary>
    /// Optional emphasized text that appears before the main text.
    /// </summary>
    /// <remarks>
    /// Allows adding a bold or highlighted section to draw more attention.
    /// </remarks>
    /// <example>
    /// <code>
    /// &lt;Alert TextEmphasis="Warning:" Text="System maintenance scheduled" /&gt;
    /// </code>
    /// </example>
    [Parameter]
    public string? TextEmphasis { get; set; }

    /// <summary>
    /// Optional custom content for complex scenarios.
    /// </summary>
    /// <remarks>
    /// Provides full flexibility for custom HTML when Text/TextEmphasis isn't sufficient.
    /// </remarks>
    /// <example>
    /// <code>
    /// &lt;Alert&gt;
    ///     &lt;CustomContent&gt;
    ///         &lt;p&gt;Custom HTML content&lt;/p&gt;
    ///         &lt;a href="#"&gt;Link&lt;/a&gt;
    ///     &lt;/CustomContent&gt;
    /// &lt;/Alert&gt;
    /// </code>
    /// </example>
    [Parameter]
    public RenderFragment? CustomContent { get; set; }

    /// <summary>
    /// The color variant of the alert.
    /// </summary>
    /// <remarks>
    /// Determines the visual style and color scheme of the alert.
    /// </remarks>
    /// <example>
    /// <code>
    /// &lt;Alert Color="AlertColor.Success"&gt;
    ///     Operation completed successfully
    /// &lt;/Alert&gt;
    /// </code>
    /// </example>
    [Parameter]
    public AlertColor Color { get; set; } = AlertColor.Info;

    /// <summary>
    /// Optional icon to display at the start of the alert.
    /// </summary>
    /// <remarks>
    /// The icon will inherit the alert's color scheme, providing visual context.
    /// </remarks>
    /// <example>
    /// <code>
    /// &lt;Alert Icon="@Icons.InfoCircle"&gt;
    ///     Informational message
    /// &lt;/Alert&gt;
    /// </code>
    /// </example>
    [Parameter]
    public IconBase? Icon { get; set; }

    /// <summary>
    /// Optional additional content to display below the main alert content.
    /// </summary>
    /// <remarks>
    /// Allows for supplementary information or actions related to the alert.
    /// </remarks>
    /// <example>
    /// <code>
    /// &lt;Alert&gt;
    ///     &lt;AdditionalContent&gt;
    ///         &lt;button&gt;Take Action&lt;/button&gt;
    ///     &lt;/AdditionalContent&gt;
    /// &lt;/Alert&gt;
    /// </code>
    /// </example>
    [Parameter]
    public RenderFragment? AdditionalContent { get; set; }

    /// <summary>
    /// Event callback for when the alert is dismissed.
    /// </summary>
    /// <remarks>
    /// Allows custom handling when the alert is closed or removed.
    /// </remarks>
    /// <accessibility>
    /// Ensures keyboard and screen reader compatibility for alert dismissal.
    /// </accessibility>
    /// <example>
    /// <code>
    /// &lt;Alert OnDismiss="@HandleAlertDismiss"&gt;
    ///     Dismissable alert
    /// &lt;/Alert&gt;
    /// 
    /// @code {
    ///     private void HandleAlertDismiss(MouseEventArgs args)
    ///     {
    ///         // Custom dismiss logic
    ///     }
    /// }
    /// </code>
    /// </example>
    [Parameter]
    public EventCallback<MouseEventArgs> OnDismiss { get; set; }

    /// <summary>
    /// Whether the alert should have rounded corners.
    /// </summary>
    /// <remarks>
    /// Adds visual softness to the alert's appearance.
    /// </remarks>
    /// <example>
    /// <code>
    /// &lt;Alert Rounded="true"&gt;
    ///     Rounded alert
    /// &lt;/Alert&gt;
    /// </code>
    /// </example>
    [Parameter]
    public bool Rounded { get; set; } = true;

    /// <summary>
    /// Whether the alert should have a colored border accent at the top.
    /// </summary>
    /// <remarks>
    /// Adds a distinctive top border that matches the alert's color scheme.
    /// </remarks>
    /// <example>
    /// <code>
    /// &lt;Alert WithBorderAccent="true"&gt;
    ///     Alert with top border accent
    /// &lt;/Alert&gt;
    /// </code>
    /// </example>
    [Parameter]
    public bool WithBorderAccent { get; set; }

    private string ClassNames => string.Join(" ", new[]
    {
        BaseClasses,
        GetColorClasses(),
        Rounded ? RoundedClasses : null,
        WithBorderAccent ? BorderAccentClasses : null,
        AdditionalAttributes?.ContainsKey("class") == true ? AdditionalAttributes["class"]?.ToString() : null
    }.Where(c => !string.IsNullOrEmpty(c)));

    private string GetColorClasses() => Color switch
    {
        AlertColor.Info => "border-primary-500 bg-primary-100 text-primary-700 dark:bg-primary-200 dark:text-primary-800",
        AlertColor.Gray => "border-gray-500 bg-gray-100 text-gray-700 dark:bg-gray-700 dark:text-gray-300",
        AlertColor.Failure => "border-red-500 bg-red-100 text-red-700 dark:bg-red-200 dark:text-red-800",
        AlertColor.Success => "border-green-500 bg-green-100 text-green-700 dark:bg-green-200 dark:text-green-800",
        AlertColor.Warning => "border-yellow-500 bg-yellow-100 text-yellow-700 dark:bg-yellow-200 dark:text-yellow-800",
        AlertColor.Red => "border-red-500 bg-red-100 text-red-700 dark:bg-red-200 dark:text-red-800",
        AlertColor.Green => "border-green-500 bg-green-100 text-green-700 dark:bg-green-200 dark:text-green-800",
        AlertColor.Yellow => "border-yellow-500 bg-yellow-100 text-yellow-700 dark:bg-yellow-200 dark:text-yellow-800",
        AlertColor.Blue => "border-blue-500 bg-blue-100 text-blue-700 dark:bg-blue-200 dark:text-blue-800",
        AlertColor.Primary => "border-primary-500 bg-primary-100 text-primary-700 dark:bg-primary-200 dark:text-primary-800",
        AlertColor.Pink => "border-pink-500 bg-pink-100 text-pink-700 dark:bg-pink-200 dark:text-pink-800",
        AlertColor.Lime => "border-lime-500 bg-lime-100 text-lime-700 dark:bg-lime-200 dark:text-lime-800",
        AlertColor.Dark => "border-gray-600 bg-gray-800 text-gray-200 dark:bg-gray-900 dark:text-gray-300",
        AlertColor.Indigo => "border-indigo-500 bg-indigo-100 text-indigo-700 dark:bg-indigo-200 dark:text-indigo-800",
        AlertColor.Purple => "border-purple-500 bg-purple-100 text-purple-700 dark:bg-purple-200 dark:text-purple-800",
        AlertColor.Teal => "border-teal-500 bg-teal-100 text-teal-700 dark:bg-teal-200 dark:text-teal-800",
        AlertColor.Light => "border-gray-400 bg-gray-50 text-gray-600 dark:bg-gray-500 dark:text-gray-200",
        _ => "border-primary-500 bg-primary-100 text-primary-700 dark:bg-primary-200 dark:text-primary-800"
    };

    private string GetIconColorClass() => Color switch
    {
        AlertColor.Info or AlertColor.Primary => "text-primary-500",
        AlertColor.Success or AlertColor.Green => "text-green-500",
        AlertColor.Warning or AlertColor.Yellow => "text-yellow-500",
        AlertColor.Failure or AlertColor.Red => "text-red-500",
        AlertColor.Blue => "text-blue-500",
        AlertColor.Pink => "text-pink-500",
        AlertColor.Lime => "text-lime-500",
        AlertColor.Dark => "text-gray-500",
        AlertColor.Indigo => "text-indigo-500",
        AlertColor.Purple => "text-purple-500",
        AlertColor.Teal => "text-teal-500",
        AlertColor.Light => "text-gray-400",
        _ => "text-gray-500"
    };

    private string GetDismissButtonClasses() => Color switch
    {
        AlertColor.Info => "-m-1.5 ml-auto inline-flex h-8 w-8 rounded-lg p-1.5 bg-primary-100 text-primary-500 hover:bg-primary-200 focus:ring-2 focus:ring-primary-400 dark:bg-primary-200 dark:text-primary-600 dark:hover:bg-primary-300",
        AlertColor.Gray => "-m-1.5 ml-auto inline-flex h-8 w-8 rounded-lg p-1.5 bg-gray-100 text-gray-500 hover:bg-gray-200 focus:ring-2 focus:ring-gray-400 dark:bg-gray-700 dark:text-gray-300 dark:hover:bg-gray-800 dark:hover:text-white",
        AlertColor.Failure => "-m-1.5 ml-auto inline-flex h-8 w-8 rounded-lg p-1.5 bg-red-100 text-red-500 hover:bg-red-200 focus:ring-2 focus:ring-red-400 dark:bg-red-200 dark:text-red-600 dark:hover:bg-red-300",
        AlertColor.Success => "-m-1.5 ml-auto inline-flex h-8 w-8 rounded-lg p-1.5 bg-green-100 text-green-500 hover:bg-green-200 focus:ring-2 focus:ring-green-400 dark:bg-green-200 dark:text-green-600 dark:hover:bg-green-300",
        AlertColor.Warning => "-m-1.5 ml-auto inline-flex h-8 w-8 rounded-lg p-1.5 bg-yellow-100 text-yellow-500 hover:bg-yellow-200 focus:ring-2 focus:ring-yellow-400 dark:bg-yellow-200 dark:text-yellow-600 dark:hover:bg-yellow-300",
        AlertColor.Red => "-m-1.5 ml-auto inline-flex h-8 w-8 rounded-lg p-1.5 bg-red-100 text-red-500 hover:bg-red-200 focus:ring-2 focus:ring-red-400 dark:bg-red-200 dark:text-red-600 dark:hover:bg-red-300",
        AlertColor.Green => "-m-1.5 ml-auto inline-flex h-8 w-8 rounded-lg p-1.5 bg-green-100 text-green-500 hover:bg-green-200 focus:ring-2 focus:ring-green-400 dark:bg-green-200 dark:text-green-600 dark:hover:bg-green-300",
        AlertColor.Yellow => "-m-1.5 ml-auto inline-flex h-8 w-8 rounded-lg p-1.5 bg-yellow-100 text-yellow-500 hover:bg-yellow-200 focus:ring-2 focus:ring-yellow-400 dark:bg-yellow-200 dark:text-yellow-600 dark:hover:bg-yellow-300",
        AlertColor.Blue => "-m-1.5 ml-auto inline-flex h-8 w-8 rounded-lg p-1.5 bg-blue-100 text-blue-500 hover:bg-blue-200 focus:ring-2 focus:ring-blue-400 dark:bg-blue-200 dark:text-blue-600 dark:hover:bg-blue-300",
        AlertColor.Primary => "-m-1.5 ml-auto inline-flex h-8 w-8 rounded-lg p-1.5 bg-primary-100 text-primary-500 hover:bg-primary-200 focus:ring-2 focus:ring-primary-400 dark:bg-primary-200 dark:text-primary-600 dark:hover:bg-primary-300",
        AlertColor.Pink => "-m-1.5 ml-auto inline-flex h-8 w-8 rounded-lg p-1.5 bg-pink-100 text-pink-500 hover:bg-pink-200 focus:ring-2 focus:ring-pink-400 dark:bg-pink-200 dark:text-pink-600 dark:hover:bg-pink-300",
        AlertColor.Lime => "-m-1.5 ml-auto inline-flex h-8 w-8 rounded-lg p-1.5 bg-lime-100 text-lime-500 hover:bg-lime-200 focus:ring-2 focus:ring-lime-400 dark:bg-lime-200 dark:text-lime-600 dark:hover:bg-lime-300",
        AlertColor.Dark => "-m-1.5 ml-auto inline-flex h-8 w-8 rounded-lg p-1.5 bg-gray-100 text-gray-500 hover:bg-gray-200 focus:ring-2 focus:ring-gray-400 dark:bg-gray-200 dark:text-gray-600 dark:hover:bg-gray-300",
        AlertColor.Indigo => "-m-1.5 ml-auto inline-flex h-8 w-8 rounded-lg p-1.5 bg-indigo-100 text-indigo-500 hover:bg-indigo-200 focus:ring-2 focus:ring-indigo-400 dark:bg-indigo-200 dark:text-indigo-600 dark:hover:bg-indigo-300",
        AlertColor.Purple => "-m-1.5 ml-auto inline-flex h-8 w-8 rounded-lg p-1.5 bg-purple-100 text-purple-500 hover:bg-purple-200 focus:ring-2 focus:ring-purple-400 dark:bg-purple-200 dark:text-purple-600 dark:hover:bg-purple-300",
        AlertColor.Teal => "-m-1.5 ml-auto inline-flex h-8 w-8 rounded-lg p-1.5 bg-teal-100 text-teal-500 hover:bg-teal-200 focus:ring-2 focus:ring-teal-400 dark:bg-teal-200 dark:text-teal-600 dark:hover:bg-teal-300",
        AlertColor.Light => "-m-1.5 ml-auto inline-flex h-8 w-8 rounded-lg p-1.5 bg-gray-50 text-gray-500 hover:bg-gray-100 focus:ring-2 focus:ring-gray-200 dark:bg-gray-600 dark:text-gray-200 dark:hover:bg-gray-700 dark:hover:text-white",
        _ => "-m-1.5 ml-auto inline-flex h-8 w-8 rounded-lg p-1.5 bg-primary-100 text-primary-500 hover:bg-primary-200 focus:ring-2 focus:ring-primary-400 dark:bg-primary-200 dark:text-primary-600 dark:hover:bg-primary-300"
    };
}

/// <summary>
/// Defines the available color variants for the Alert component.
/// </summary>
/// <remarks>
/// Provides a comprehensive set of color options to convey different types of messages.
/// </remarks>
public enum AlertColor
{
    /// <summary>
    /// Informational alert with a primary color scheme.
    /// </summary>
    Info,

    /// <summary>
    /// Neutral gray alert for general messages.
    /// </summary>
    Gray,

    /// <summary>
    /// Alert indicating a failure or error condition.
    /// </summary>
    Failure,

    /// <summary>
    /// Alert indicating a successful operation.
    /// </summary>
    Success,

    /// <summary>
    /// Alert indicating a warning or potential issue.
    /// </summary>
    Warning,

    /// <summary>
    /// Red-colored alert for critical messages.
    /// </summary>
    Red,

    /// <summary>
    /// Green-colored alert for positive messages.
    /// </summary>
    Green,

    /// <summary>
    /// Yellow-colored alert for cautionary messages.
    /// </summary>
    Yellow,

    /// <summary>
    /// Blue-colored alert for informational messages.
    /// </summary>
    Blue,

    /// <summary>
    /// Primary color alert (typically matching the application's primary theme).
    /// </summary>
    Primary,

    /// <summary>
    /// Pink-colored alert.
    /// </summary>
    Pink,

    /// <summary>
    /// Lime-colored alert.
    /// </summary>
    Lime,

    /// <summary>
    /// Dark-themed alert with a gray/black color scheme.
    /// </summary>
    Dark,

    /// <summary>
    /// Indigo-colored alert.
    /// </summary>
    Indigo,

    /// <summary>
    /// Purple-colored alert.
    /// </summary>
    Purple,

    /// <summary>
    /// Teal-colored alert.
    /// </summary>
    Teal,

    /// <summary>
    /// Light-colored alert with a subtle background.
    /// </summary>
    Light
}
