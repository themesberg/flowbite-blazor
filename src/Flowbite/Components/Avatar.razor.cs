using Flowbite.Components.Base;

namespace Flowbite.Components;

public partial class Avatar
{
    /// <summary>
    /// Alternative text for the avatar image
    /// </summary>
    [Parameter]
    public string? Alt { get; set; }

    /// <summary>
    /// Whether the avatar should have a border
    /// </summary>
    [Parameter]
    public bool Bordered { get; set; }

    /// <summary>
    /// URL of the avatar image
    /// </summary>
    [Parameter]
    public string? ImageUrl { get; set; }

    /// <summary>
    /// Color variant of the avatar
    /// </summary>
    [Parameter]
    public AvatarColor Color { get; set; } = AvatarColor.Light;

    /// <summary>
    /// Whether the avatar should be rounded
    /// </summary>
    [Parameter]
    public bool Rounded { get; set; }

    /// <summary>
    /// Size variant of the avatar
    /// </summary>
    [Parameter]
    public AvatarSize Size { get; set; } = AvatarSize.Medium;

    /// <summary>
    /// Whether the avatar should be stacked (used in groups)
    /// </summary>
    [Parameter]
    public bool Stacked { get; set; }

    /// <summary>
    /// Status indicator for the avatar (online, offline, away, busy)
    /// </summary>
    [Parameter]
    public string? Status { get; set; }

    /// <summary>
    /// Position of the status indicator
    /// </summary>
    [Parameter]
    public string StatusPosition { get; set; } = "top-left";

    /// <summary>
    /// Initials to display when no image is provided
    /// </summary>
    [Parameter]
    public string? PlaceholderInitials { get; set; }

    /// <summary>
    /// Additional content to be rendered with the avatar
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    private string GetImageClasses()
    {
        var classes = new List<string>
        {
            "relative inline-block",
            GetSizeClass(),
        };

        if (Bordered)
        {
            classes.Add("ring-2 ring-gray-300 dark:ring-gray-500");
            classes.Add(GetColorClass());
        }

        if (Rounded)
        {
            classes.Add("rounded-full");
        }

        if (Stacked)
        {
            classes.Add("-ml-4 border-2 border-white dark:border-gray-800");
        }

        return string.Join(" ", classes);
    }

    private string GetSizeClass() => Size switch
    {
        AvatarSize.ExtraSmall => "w-6 h-6",
        AvatarSize.Small => "w-8 h-8",
        AvatarSize.Medium => "w-10 h-10",
        AvatarSize.Large => "w-20 h-20",
        AvatarSize.ExtraLarge => "w-36 h-36",
        _ => "w-10 h-10"
    };

    private string GetColorClass() => Color switch
    {
        AvatarColor.Gray => "ring-gray-300 dark:ring-gray-500",
        AvatarColor.Success => "ring-green-300 dark:ring-green-500",
        AvatarColor.Pink => "ring-pink-300 dark:ring-pink-500",
        AvatarColor.Purple => "ring-purple-300 dark:ring-purple-500",
        AvatarColor.Info => "ring-blue-300 dark:ring-blue-500",
        AvatarColor.Warning => "ring-yellow-300 dark:ring-yellow-500",
        AvatarColor.Failure => "ring-red-300 dark:ring-red-500",
        _ => "ring-gray-300 dark:ring-gray-500"
    };

    /// <summary>
    /// Defines the color variants for avatars.
    /// </summary>
    public enum AvatarColor
    {
        /// <summary>
        /// Default light color.
        /// </summary>
        Light,

        /// <summary>
        /// Gray color variant.
        /// </summary>
        Gray,

        /// <summary>
        /// Success (green) color variant.
        /// </summary>
        Success,

        /// <summary>
        /// Pink color variant.
        /// </summary>
        Pink,

        /// <summary>
        /// Purple color variant.
        /// </summary>
        Purple,

        /// <summary>
        /// Info (blue) color variant.
        /// </summary>
        Info,

        /// <summary>
        /// Warning (yellow) color variant.
        /// </summary>
        Warning,

        /// <summary>
        /// Failure (red) color variant.
        /// </summary>
        Failure
    }

    /// <summary>
    /// Defines the size variants for avatars.
    /// </summary>
    public enum AvatarSize
    {
        /// <summary>
        /// Extra small size.
        /// </summary>
        ExtraSmall,

        /// <summary>
        /// Small size.
        /// </summary>
        Small,

        /// <summary>
        /// Medium size (default).
        /// </summary>
        Medium,

        /// <summary>
        /// Large size.
        /// </summary>
        Large,

        /// <summary>
        /// Extra large size.
        /// </summary>
        ExtraLarge
    }

    private string GetStatusClasses()
    {
        var classes = new List<string>
        {
            "absolute h-3.5 w-3.5 rounded-full border-2 border-white dark:border-gray-800"
        };

        classes.Add(GetStatusPositionClass());
        classes.Add(GetStatusColorClass());

        return string.Join(" ", classes);
    }

    private string GetStatusColorClass() => Status switch
    {
        "online" => "bg-green-400",
        "offline" => "bg-gray-400",
        "away" => "bg-yellow-400",
        "busy" => "bg-red-400",
        _ => "bg-gray-400"
    };

    private string GetStatusPositionClass() => StatusPosition switch
    {
        "bottom-left" => "bottom-0 left-0",
        "bottom-center" => "bottom-0 left-1/2 -translate-x-1/2",
        "bottom-right" => "bottom-0 right-0",
        "top-left" => "top-0 left-0",
        "top-center" => "top-0 left-1/2 -translate-x-1/2",
        "top-right" => "top-0 right-0",
        "center-left" => "top-1/2 -translate-y-1/2 left-0",
        "center-right" => "top-1/2 -translate-y-1/2 right-0",
        _ => "top-0 left-0"
    };

    private string GetInitialsClasses()
    {
        var classes = new List<string>
        {
            "relative inline-flex items-center justify-center",
            GetSizeClass(),
            "bg-gray-100 dark:bg-gray-600 text-gray-600 dark:text-gray-300"
        };

        if (Bordered)
        {
            classes.Add("ring-2");
            classes.Add(GetColorClass());
        }

        if (Rounded)
        {
            classes.Add("rounded-full");
        }

        if (Stacked)
        {
            classes.Add("-ml-4 border-2 border-white dark:border-gray-800");
        }

        return string.Join(" ", classes);
    }

    private string GetInitialsTextClass() => Size switch
    {
        AvatarSize.ExtraSmall => "text-xs",
        AvatarSize.Small => "text-sm",
        AvatarSize.Medium => "text-base",
        AvatarSize.Large => "text-xl",
        AvatarSize.ExtraLarge => "text-2xl",
        _ => "text-base"
    };
}
