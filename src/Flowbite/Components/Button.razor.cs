using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Collections.Generic;
using Flowbite.Components.Base;
using Microsoft.AspNetCore.Components.Routing;

namespace Flowbite.Components;

/// <summary>
/// Partial class for the Button component, providing additional logic and code-behind functionality.
/// </summary>
public partial class Button
{
    /// <summary>
    /// The type of button (e.g., "button", "submit", "reset").
    /// Only used when not rendering as a link.
    /// </summary>
    [Parameter]
    public string Type { get; set; } = "button";

    /// <summary>
    /// The URL that the button links to. If provided, the button will render as an anchor tag.
    /// </summary>
    [Parameter]
    public string? Href { get; set; }

    /// <summary>
    /// The target attribute for the link (e.g., "_blank", "_self").
    /// Only used when Href is provided.
    /// </summary>
    [Parameter]
    public string? Target { get; set; }

    /// <summary>
    /// Determines if the button is disabled.
    /// </summary>
    [Parameter]
    public bool Disabled { get; set; }

    /// <summary>
    /// Indicates if the button is in a loading state.
    /// </summary>
    [Parameter]
    public bool Loading { get; set; }

    /// <summary>
    /// Indicates if the button should use fully rounded corners.
    /// </summary>
    [Parameter]
    public bool Pill { get; set; }

    /// <summary>
    /// The visual style of the button.
    /// </summary>
    [Parameter]
    public ButtonStyle Style { get; set; } = ButtonStyle.Default;

    /// <summary>
    /// The size of the button.
    /// </summary>
    [Parameter]
    public ButtonSize Size { get; set; } = ButtonSize.Medium;

    /// <summary>
    /// The color variant of the button.
    /// </summary>
    [Parameter]
    public ButtonColor Color { get; set; } = ButtonColor.Default;

    /// <summary>
    /// The icon to display in the button.
    /// </summary>
    [Parameter]
    public IconBase? Icon { get; set; }

    [Parameter]
    public string OverrideIconTextColor { get; set; } = "text-white";

    [Parameter]
    public string OverrideIconSize { get; set; } = "w-6 h-6";

    /// <summary>
    /// Child content of the button.
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// Callback for button click event.
    /// </summary>
    [Parameter]
    public EventCallback<MouseEventArgs> OnClick { get; set; }

    /// <summary>
    /// Additional attributes to be applied to the button element.
    /// </summary>
    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object>? AdditionalAttributes { get; set; }


    private string IconCssClassOverrides => $"{OverrideIconTextColor} {OverrideIconSize}";

    /// <summary>
    /// Handles the button click event, preventing default action if the button is disabled or loading.
    /// </summary>
    /// <param name="args">Mouse event arguments</param>
    private async Task HandleClick(MouseEventArgs args)
    {
        if (Disabled || Loading)
            return;

        if (OnClick.HasDelegate)
            await OnClick.InvokeAsync(args);
    }

    /// <summary>
    /// Generates the appropriate CSS classes for the button based on its properties.
    /// </summary>
    private string GetButtonClasses()
    {
        var classes = new List<string>
        {
            "focus:outline-none",
            "inline-flex",
            "items-center",
            "justify-center",
            "font-medium",
            "text-center",
            Pill ? "rounded-full" : "rounded-lg",
            "group"
        };

        // Size classes
        classes.AddRange(Size switch
        {
            ButtonSize.Small => new[] { "text-xs", "px-3", "py-2" },
            ButtonSize.Medium => new[] { "text-sm", "px-5", "py-2.5" },
            ButtonSize.Large => new[] { "text-base", "px-6", "py-3" },
            _ => new[] { "text-sm", "px-5", "py-2.5" }
        });

        // Color and style classes
        classes.AddRange(GetColorClasses());

        // Disabled state
        if (Disabled)
        {
            classes.Add("cursor-not-allowed");
            classes.Add("opacity-50");
        }

        // Loading state
        if (Loading)
        {
            classes.Add("cursor-wait");
            classes.Add("opacity-75");
        }

        return CombineClasses(string.Join(" ", classes)) ?? string.Empty;
    }

    /// <summary>
    /// Generates color-specific classes based on button style and color.
    /// </summary>
    private IEnumerable<string> GetColorClasses()
    {
        return Style switch
        {
            ButtonStyle.Default => Color switch
            {
                ButtonColor.Default => new[] { "text-white", "bg-blue-700", "hover:bg-blue-800", "focus:ring-4", "focus:ring-blue-300", "dark:bg-blue-600", "dark:hover:bg-blue-700", "dark:focus:ring-blue-800" },
                ButtonColor.Gray => new[] { "text-gray-900", "bg-white", "border", "border-gray-200", "hover:bg-gray-100", "focus:ring-4", "focus:ring-gray-100", "dark:bg-gray-800", "dark:text-white", "dark:border-gray-600", "dark:hover:bg-gray-700", "dark:focus:ring-gray-700" },
                ButtonColor.Dark => new[] { "text-white", "bg-gray-800", "hover:bg-gray-900", "focus:ring-4", "focus:ring-gray-300", "dark:bg-gray-800", "dark:hover:bg-gray-700", "dark:focus:ring-gray-700" },
                ButtonColor.Light => new[] { "text-gray-900", "bg-white", "border", "border-gray-300", "hover:bg-gray-100", "focus:ring-4", "focus:ring-gray-100", "dark:bg-gray-800", "dark:text-white", "dark:border-gray-600", "dark:hover:bg-gray-700", "dark:focus:ring-gray-700" },
                ButtonColor.Green => new[] { "text-white", "bg-green-700", "hover:bg-green-800", "focus:ring-4", "focus:ring-green-300", "dark:bg-green-600", "dark:hover:bg-green-700", "dark:focus:ring-green-800" },
                ButtonColor.Red => new[] { "text-white", "bg-red-700", "hover:bg-red-800", "focus:ring-4", "focus:ring-red-300", "dark:bg-red-600", "dark:hover:bg-red-700", "dark:focus:ring-red-800" },
                ButtonColor.Yellow => new[] { "text-white", "bg-yellow-400", "hover:bg-yellow-500", "focus:ring-4", "focus:ring-yellow-300", "dark:focus:ring-yellow-900" },
                ButtonColor.Purple => new[] { "text-white", "bg-purple-700", "hover:bg-purple-800", "focus:ring-4", "focus:ring-purple-300", "dark:bg-purple-600", "dark:hover:bg-purple-700", "dark:focus:ring-purple-900" },
                _ => new[] { "text-white", "bg-blue-700", "hover:bg-blue-800", "focus:ring-4", "focus:ring-blue-300", "dark:bg-blue-600", "dark:hover:bg-blue-700", "dark:focus:ring-blue-800" }
            },
            ButtonStyle.Outline => Color switch
            {
                ButtonColor.Default => new[] { "text-blue-700", "border", "border-blue-700", "hover:bg-blue-700", "hover:text-white", "focus:ring-4", "focus:ring-blue-300", "dark:border-blue-500", "dark:text-blue-500", "dark:hover:text-white", "dark:hover:bg-blue-500", "dark:focus:ring-blue-800" },
                ButtonColor.Gray => new[] { "text-gray-900", "border", "border-gray-800", "hover:bg-gray-900", "hover:text-white", "focus:ring-4", "focus:ring-gray-300", "dark:border-gray-600", "dark:text-gray-400", "dark:hover:text-white", "dark:hover:bg-gray-600", "dark:focus:ring-gray-700" },
                ButtonColor.Dark => new[] { "text-gray-900", "border", "border-gray-800", "hover:bg-gray-900", "hover:text-white", "focus:ring-4", "focus:ring-gray-300", "dark:border-gray-600", "dark:text-gray-400", "dark:hover:text-white", "dark:hover:bg-gray-600", "dark:focus:ring-gray-700" },
                ButtonColor.Light => new[] { "text-gray-500", "border", "border-gray-300", "hover:bg-gray-100", "focus:ring-4", "focus:ring-gray-200", "dark:border-gray-600", "dark:text-gray-400", "dark:hover:text-white", "dark:hover:bg-gray-600", "dark:focus:ring-gray-700" },
                ButtonColor.Green => new[] { "text-green-700", "border", "border-green-700", "hover:bg-green-700", "hover:text-white", "focus:ring-4", "focus:ring-green-300", "dark:border-green-500", "dark:text-green-500", "dark:hover:text-white", "dark:hover:bg-green-500", "dark:focus:ring-green-800" },
                ButtonColor.Red => new[] { "text-red-700", "border", "border-red-700", "hover:bg-red-700", "hover:text-white", "focus:ring-4", "focus:ring-red-300", "dark:border-red-500", "dark:text-red-500", "dark:hover:text-white", "dark:hover:bg-red-500", "dark:focus:ring-red-800" },
                ButtonColor.Yellow => new[] { "text-yellow-400", "border", "border-yellow-400", "hover:bg-yellow-400", "hover:text-white", "focus:ring-4", "focus:ring-yellow-300", "dark:border-yellow-300", "dark:text-yellow-300", "dark:hover:text-white", "dark:hover:bg-yellow-300", "dark:focus:ring-yellow-300" },
                ButtonColor.Purple => new[] { "text-purple-700", "border", "border-purple-700", "hover:bg-purple-700", "hover:text-white", "focus:ring-4", "focus:ring-purple-300", "dark:border-purple-500", "dark:text-purple-500", "dark:hover:text-white", "dark:hover:bg-purple-500", "dark:focus:ring-purple-800" },
                _ => new[] { "text-blue-700", "border", "border-blue-700", "hover:bg-blue-700", "hover:text-white", "focus:ring-4", "focus:ring-blue-300", "dark:border-blue-500", "dark:text-blue-500", "dark:hover:text-white", "dark:hover:bg-blue-500", "dark:focus:ring-blue-800" }
            },
            _ => Color switch
            {
                ButtonColor.Default => new[] { "text-white", "bg-blue-700", "hover:bg-blue-800", "focus:ring-4", "focus:ring-blue-300", "dark:bg-blue-600", "dark:hover:bg-blue-700", "dark:focus:ring-blue-800" },
                _ => new[] { "text-white", "bg-blue-700", "hover:bg-blue-800", "focus:ring-4", "focus:ring-blue-300", "dark:bg-blue-600", "dark:hover:bg-blue-700", "dark:focus:ring-blue-800" }
            }
        };
    }

    /// <summary>
    /// Defines the visual style of a button.
    /// </summary>
    public enum ButtonStyle
    {
        /// <summary>
        /// Default filled button style.
        /// </summary>
        Default,

        /// <summary>
        /// Outline button style with transparent background.
        /// </summary>
        Outline
    }

    /// <summary>
    /// Defines the color variants for buttons.
    /// </summary>
    public enum ButtonColor
    {
        /// <summary>
        /// Default blue color.
        /// </summary>
        Default,

        /// <summary>
        /// Gray light/gray color.
        /// </summary>
        Gray,

        /// <summary>
        /// Dark color variant.
        /// </summary>
        Dark,

        /// <summary>
        /// Light color variant.
        /// </summary>
        Light,

        /// <summary>
        /// Green color variant.
        /// </summary>
        Green,

        /// <summary>
        /// Red color variant.
        /// </summary>
        Red,

        /// <summary>
        /// Yellow color variant.
        /// </summary>
        Yellow,

        /// <summary>
        /// Purple color variant.
        /// </summary>
        Purple
    }

    /// <summary>
    /// Defines the size variants for buttons.
    /// </summary>
    public enum ButtonSize
    {
        /// <summary>
        /// Small button size.
        /// </summary>
        Small,

        /// <summary>
        /// Medium button size (default).
        /// </summary>
        Medium,

        /// <summary>
        /// Large button size.
        /// </summary>
        Large
    }
}

