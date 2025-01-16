using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Flowbite.Base;


namespace Flowbite.Components;

/// <summary>
/// Represents an individual item within a dropdown menu.
/// </summary>
/// <remarks>
/// Provides a flexible and interactive dropdown item with support for icons, disabled states, and custom click handling.
/// </remarks>
public partial class DropdownItem
{
    /// <summary>
    /// The content to be displayed in the dropdown item.
    /// </summary>
    /// <remarks>
    /// Allows for custom content rendering within the dropdown item.
    /// </remarks>
    /// <example>
    /// <code>
    /// &lt;DropdownItem&gt;
    ///     Edit Profile
    /// &lt;/DropdownItem&gt;
    /// 
    /// &lt;DropdownItem&gt;
    ///     &lt;span class="text-red-500"&gt;Delete Account&lt;/span&gt;
    /// &lt;/DropdownItem&gt;
    /// </code>
    /// </example>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// Determines whether the dropdown item is disabled.
    /// </summary>
    /// <remarks>
    /// When true, prevents click events and applies a visually muted style.
    /// </remarks>
    /// <accessibility>
    /// Ensures that disabled items are not focusable and have appropriate aria attributes.
    /// </accessibility>
    /// <example>
    /// <code>
    /// &lt;DropdownItem Disabled="true"&gt;
    ///     Unavailable Option
    /// &lt;/DropdownItem&gt;
    /// </code>
    /// </example>
    [Parameter]
    public bool Disabled { get; set; }

    /// <summary>
    /// Optional icon to display at the start of the dropdown item.
    /// </summary>
    /// <remarks>
    /// Provides a visual indicator or context for the dropdown item.
    /// The icon's color adapts based on the item's state (enabled/disabled).
    /// </remarks>
    /// <example>
    /// <code>
    /// &lt;DropdownItem Icon="@Icons.Edit"&gt;
    ///     Edit Profile
    /// &lt;/DropdownItem&gt;
    /// </code>
    /// </example>
    [Parameter]
    public IconBase? Icon { get; set; }

    /// <summary>
    /// Event callback triggered when the dropdown item is clicked.
    /// </summary>
    /// <remarks>
    /// Allows custom handling of click events. Will not trigger if the item is disabled.
    /// </remarks>
    /// <accessibility>
    /// Ensures keyboard and screen reader compatibility for dropdown item interactions.
    /// </accessibility>
    /// <example>
    /// <code>
    /// &lt;DropdownItem OnClick="@HandleItemClick"&gt;
    ///     Edit Profile
    /// &lt;/DropdownItem&gt;
    /// 
    /// @code {
    ///     private async Task HandleItemClick(MouseEventArgs args)
    ///     {
    ///         // Custom click handling logic
    ///     }
    /// }
    /// </code>
    /// </example>
    [Parameter]
    public EventCallback<MouseEventArgs> OnClick { get; set; }

    /// <summary>
    /// Additional attributes to be applied to the dropdown item.
    /// </summary>
    /// <remarks>
    /// Allows for custom HTML attributes to be added to the dropdown item element.
    /// </remarks>
    /// <example>
    /// <code>
    /// &lt;DropdownItem data-testid="profile-edit"&gt;
    ///     Edit Profile
    /// &lt;/DropdownItem&gt;
    /// </code>
    /// </example>
    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object>? AdditionalAttributes { get; set; }

    private async Task HandleClick(MouseEventArgs args)
    {
        if (!Disabled)
        {
            await OnClick.InvokeAsync(args);
        }
    }

    private string GetItemClasses()
    {
        var baseClasses = "flex w-full items-center px-4 py-2 text-sm";
        var stateClasses = !Disabled 
            ? "cursor-pointer text-gray-700 dark:text-gray-200 hover:bg-gray-100 focus:bg-gray-100 dark:hover:bg-gray-600 dark:focus:bg-gray-600 dark:hover:text-white dark:focus:text-white focus:outline-none" 
            : "text-gray-400 dark:text-gray-500 cursor-not-allowed";
        var iconClasses = Icon != null ? "items-center" : "";
        var additionalClasses = AdditionalAttributes?.ContainsKey("class") == true 
            ? AdditionalAttributes["class"]?.ToString() 
            : null;

        return CombineClasses(string.Join(" ", new[]
        {
            baseClasses,
            stateClasses,
            iconClasses, 
            additionalClasses
        }));
    }

    private string GetIconClasses()
    {
        return CombineClasses(string.Join(" ", new[]
        {
            "mr-2 h-4 w-4",
            Disabled ? "text-gray-400 dark:text-gray-500" : "text-gray-500 dark:text-gray-400",
        }));
    }
}
