using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Flowbite.Base;
using Flowbite.Utilities;

namespace Flowbite.Components;

/// <summary>
/// Represents an individual item within a dropdown menu.
/// </summary>
/// <remarks>
/// Provides a flexible and interactive dropdown item with support for icons, disabled states, and custom click handling.
/// Implements keyboard navigation support with visual focus indicators.
/// </remarks>
public partial class DropdownItem : IDisposable
{
    private bool _disposed;
    private string? _textContent;

    [CascadingParameter]
    private Dropdown ParentDropdown { get; set; } = default!;

    /// <summary>
    /// Plain text content for type-ahead search and accessibility.
    /// </summary>
    /// <remarks>
    /// If not provided, the component will try to extract text from ChildContent.
    /// For complex content, explicitly set this parameter.
    /// </remarks>
    [Parameter]
    public string? Text { get; set; }

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

    private async Task HandleClick(MouseEventArgs args)
    {
        if (Disabled) return;

        // Execute click handler first
        if (OnClick.HasDelegate)
        {
            await OnClick.InvokeAsync(args);
        }

        // Close dropdown after click handler completes
        if (ParentDropdown.DismissOnClick)
        {
            await ParentDropdown.CloseDropdown();
        }
    }

    private string GetItemClasses()
    {
        return MergeClasses(
            ElementClass.Empty()
                .Add("flex w-full items-center px-4 py-2 text-sm")
                .Add("cursor-pointer text-gray-700 dark:text-gray-200 hover:bg-gray-100 dark:hover:bg-gray-600 dark:hover:text-white focus:outline-none", when: !Disabled)
                // Focus ring styles (Flowbite default) - applied on keyboard focus
                .Add("ring-2 ring-inset ring-blue-500 bg-gray-100 dark:bg-gray-600 dark:text-white", when: IsFocused && !Disabled)
                .Add("text-gray-400 dark:text-gray-500 cursor-not-allowed", when: Disabled)
                .Add("items-center", when: Icon != null)
                .Add(ParentDropdown?.ItemSlotClasses)
                .Add(Class)
        );
    }

    private string GetIconClasses()
    {
        return MergeClasses(
            ElementClass.Empty()
                .Add("mr-2 h-4 w-4")
                .Add("text-gray-400 dark:text-gray-500", when: Disabled)
                .Add("text-gray-500 dark:text-gray-400", when: !Disabled)
        );
    }

    /// <inheritdoc />
    protected override void OnInitialized()
    {
        base.OnInitialized();
        ParentDropdown?.RegisterItem(this);
    }

    /// <summary>
    /// Gets the text content for type-ahead search.
    /// </summary>
    internal string? GetTextContent()
    {
        return Text ?? _textContent;
    }

    /// <summary>
    /// Sets the text content extracted from ChildContent.
    /// </summary>
    internal void SetTextContent(string? text)
    {
        _textContent = text;
    }

    /// <summary>
    /// Invokes the click handler programmatically (for keyboard selection).
    /// </summary>
    internal async Task InvokeClick()
    {
        await HandleClick(new MouseEventArgs());
    }

    /// <summary>
    /// Gets whether this item is currently focused via keyboard navigation.
    /// </summary>
    private bool IsFocused => ParentDropdown?.IsItemFocused(this) == true;

    /// <summary>
    /// Gets the tabindex for this item based on focus state.
    /// </summary>
    private int GetTabIndex()
    {
        // Roving tabindex: only the focused item or no item should be tabbable
        if (Disabled) return -1;
        return IsFocused ? 0 : -1;
    }

    /// <inheritdoc />
    public void Dispose()
    {
        if (!_disposed)
        {
            _disposed = true;
            ParentDropdown?.UnregisterItem(this);
        }
    }
}
