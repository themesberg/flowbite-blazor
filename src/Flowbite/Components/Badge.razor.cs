using Flowbite.Components.Base;

namespace Flowbite.Components;

public partial class Badge
{
    /// <summary>
    /// Gets the base CSS classes for the badge component.
    /// </summary>
    private string BaseClass => "flex h-fit items-center gap-1 font-semibold";

     
    /// <summary>
    /// Gets the color-specific CSS classes based on the selected color.
    /// </summary>
    private string ColorClasses => Color switch
    {
        BadgeColor.Primary => "bg-primary-100 text-primary-800 dark:bg-primary-900 dark:text-primary-300",
        BadgeColor.Info => "bg-blue-100 text-blue-800 dark:bg-blue-900 dark:text-blue-300",
        BadgeColor.Gray => "bg-gray-100 text-gray-800 dark:bg-gray-700 dark:text-gray-300",
        BadgeColor.Failure => "bg-red-100 text-red-800 dark:bg-red-900 dark:text-red-300",
        BadgeColor.Success => "bg-green-100 text-green-800 dark:bg-green-900 dark:text-green-300",
        BadgeColor.Warning => "bg-yellow-100 text-yellow-800 dark:bg-yellow-900 dark:text-yellow-300",
        BadgeColor.Indigo => "bg-indigo-100 text-indigo-800 dark:bg-indigo-900 dark:text-indigo-300",
        BadgeColor.Purple => "bg-purple-100 text-purple-800 dark:bg-purple-900 dark:text-purple-300",
        BadgeColor.Pink => "bg-pink-100 text-pink-800 dark:bg-pink-900 dark:text-pink-300",
        _ => "bg-primary-100 text-primary-800 dark:bg-primary-900 dark:text-primary-300"
    };

    private string SizeClasses => Size switch
    {
        BadgeSize.ExtraSmall => ChildContent == null && Icon != null 
            ? "p-1.5" // Icon-only badge
            : "text-xs px-2 py-0.5",
        BadgeSize.Small => ChildContent == null && Icon != null 
            ? "p-1.5" // Icon-only badge
            : "p-1 text-sm px-2.5 py-0.5",
        _ => "p-1 text-xs px-2 py-0.5"
    };

    private string IconClass => Size switch
    {
        BadgeSize.ExtraSmall => "w-3 h-3",
        BadgeSize.Small => "w-3.5 h-3.5",
        _ => "w-3 h-3"
    };

    /// <summary>
    /// Gets the color-specific classes for the icon based on the badge color.
    /// </summary>
    private string GetIconColorClass() => Color switch
    {
        BadgeColor.Primary => "text-primary-800 dark:text-primary-300",
        BadgeColor.Info => "text-blue-800 dark:text-blue-300",
        BadgeColor.Gray => "text-gray-800 dark:text-gray-300",
        BadgeColor.Failure => "text-red-800 dark:text-red-300",
        BadgeColor.Success => "text-green-800 dark:text-green-300",
        BadgeColor.Warning => "text-yellow-800 dark:text-yellow-300",
        BadgeColor.Indigo => "text-indigo-800 dark:text-indigo-300",
        BadgeColor.Purple => "text-purple-800 dark:text-purple-300",
        BadgeColor.Pink => "text-pink-800 dark:text-pink-300",
        _ => "text-primary-800 dark:text-primary-300"
    };

    private string HrefClass => "hover:no-underline";

    private string BadgeClass => string.Join(" ", new[]
    {
        BaseClass,
        ColorClasses,
        SizeClasses,
        ChildContent == null && Icon != null ? "rounded-full" : "rounded",
        Class
    }.Where(c => !string.IsNullOrWhiteSpace(c)));

    /// <summary>
    /// Gets or sets the color of the badge.
    /// </summary>
    [Parameter]
    public BadgeColor Color { get; set; } = BadgeColor.Primary;

    /// <summary>
    /// Gets or sets the URL that the badge links to.
    /// </summary>
    [Parameter]
    public string? Href { get; set; }

    /// <summary>
    /// Optional icon to display at the start of the alert.
    /// The icon will inherit the alert's color scheme.
    /// </summary>
    [Parameter]
    public IconBase? Icon { get; set; }

    /// <summary>
    /// Gets or sets the size of the badge.
    /// </summary>
    [Parameter]
    public BadgeSize Size { get; set; } = BadgeSize.ExtraSmall;

    /// <summary>
    /// Defines the color variants for badges.
    /// </summary>
    public enum BadgeColor
    {
        /// <summary>
        /// Primary theme color (default).
        /// </summary>
        Primary,

        /// <summary>
        /// Blue color variant for informational states.
        /// </summary>
        Info,

        /// <summary>
        /// Gray color variant.
        /// </summary>
        Gray,

        /// <summary>
        /// Red color variant for failure states.
        /// </summary>
        Failure,

        /// <summary>
        /// Green color variant for success states.
        /// </summary>
        Success,

        /// <summary>
        /// Yellow color variant for warning states.
        /// </summary>
        Warning,

        /// <summary>
        /// Indigo color variant.
        /// </summary>
        Indigo,

        /// <summary>
        /// Purple color variant.
        /// </summary>
        Purple,

        /// <summary>
        /// Pink color variant.
        /// </summary>
        Pink
    }

    /// <summary>
    /// Defines the size variants for badges.
    /// </summary>
    public enum BadgeSize
    {
        /// <summary>
        /// Extra small size (default).
        /// </summary>
        ExtraSmall,

        /// <summary>
        /// Small size.
        /// </summary>
        Small
    }

    /// <summary>
    /// Gets or sets the content to be rendered inside the badge.
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// Additional attributes to be applied to the badge element.
    /// </summary>
    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object>? AdditionalAttributes { get; set; }
}
