using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Flowbite.Base;
using Flowbite.Utilities;

namespace Flowbite.Components;

/// <summary>
/// Represents a navigation link within a navbar, with support for active states, dropdowns, and click events.
/// </summary>
/// <remarks>
/// Provides a flexible navigation link component that can be used in responsive navbar layouts,
/// supporting various states like active, disabled, and dropdown functionality.
/// </remarks>
public partial class NavbarLink : FlowbiteComponentBase
{
    /// <summary>
    /// The URL that the link points to.
    /// </summary>
    /// <remarks>
    /// Specifies the destination URL when the link is clicked. 
    /// If not provided, the link will behave as a non-navigational element.
    /// </remarks>
    /// <example>
    /// <code>
    /// &lt;NavbarLink Href="/home"&gt;Home&lt;/NavbarLink&gt;
    /// &lt;NavbarLink Href="/about"&gt;About Us&lt;/NavbarLink&gt;
    /// </code>
    /// </example>
    [Parameter]
    public string? Href { get; set; }

    /// <summary>
    /// Determines if the link is in an active state.
    /// </summary>
    /// <remarks>
    /// When true, applies a distinct visual style to indicate the current or selected page.
    /// </remarks>
    /// <accessibility>
    /// Helps screen reader users understand the current navigation context.
    /// </accessibility>
    /// <example>
    /// <code>
    /// &lt;NavbarLink Href="/home" Active="true"&gt;Home&lt;/NavbarLink&gt;
    /// </code>
    /// </example>
    [Parameter]
    public bool Active { get; set; }

    /// <summary>
    /// Determines if the link is in a disabled state.
    /// </summary>
    /// <remarks>
    /// When true, prevents click events and applies a visually muted style.
    /// </remarks>
    /// <accessibility>
    /// Ensures that disabled links are not focusable and have appropriate aria attributes.
    /// </accessibility>
    /// <example>
    /// <code>
    /// &lt;NavbarLink Href="/admin" Disabled="true"&gt;Admin Panel&lt;/NavbarLink&gt;
    /// </code>
    /// </example>
    [Parameter]
    public bool Disabled { get; set; }

    /// <summary>
    /// Child content to be rendered inside the link.
    /// </summary>
    /// <remarks>
    /// Allows flexible content rendering within the navigation link.
    /// </remarks>
    /// <example>
    /// <code>
    /// &lt;NavbarLink&gt;
    ///     &lt;span&gt;Profile&lt;/span&gt;
    /// &lt;/NavbarLink&gt;
    /// </code>
    /// </example>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// Determines if this link has a dropdown menu.
    /// </summary>
    /// <remarks>
    /// When true, enables dropdown functionality for the navigation link.
    /// </remarks>
    /// <example>
    /// <code>
    /// &lt;NavbarLink HasDropdown="true"&gt;
    ///     Services
    ///     &lt;DropdownContent&gt;
    ///         &lt;DropdownItem&gt;Web Design&lt;/DropdownItem&gt;
    ///         &lt;DropdownItem&gt;Consulting&lt;/DropdownItem&gt;
    ///     &lt;/DropdownContent&gt;
    /// &lt;/NavbarLink&gt;
    /// </code>
    /// </example>
    [Parameter]
    public bool HasDropdown { get; set; }

    /// <summary>
    /// Content to be rendered inside the dropdown menu.
    /// </summary>
    /// <remarks>
    /// Allows custom content for the dropdown menu associated with the navigation link.
    /// </remarks>
    [Parameter]
    public RenderFragment? DropdownContent { get; set; }

    /// <summary>
    /// Callback for when the link is clicked.
    /// </summary>
    /// <remarks>
    /// Allows custom handling of link click events. Will not trigger if the link is disabled.
    /// </remarks>
    /// <accessibility>
    /// Ensures keyboard and screen reader compatibility for link interactions.
    /// </accessibility>
    /// <example>
    /// <code>
    /// &lt;NavbarLink OnClick="@HandleLinkClick"&gt;
    ///     Profile
    /// &lt;/NavbarLink&gt;
    /// 
    /// @code {
    ///     private async Task HandleLinkClick(MouseEventArgs args)
    ///     {
    ///         // Custom click handling logic
    ///     }
    /// }
    /// </code>
    /// </example>
    [Parameter]
    public EventCallback<MouseEventArgs> OnClick { get; set; }

    /// <summary>
    /// Gets or sets whether the dropdown menu is open.
    /// </summary>
    /// <remarks>
    /// Controls the visibility of the dropdown menu associated with the navigation link.
    /// </remarks>
    [Parameter]
    public bool IsDropdownOpen { get; set; }

    /// <summary>
    /// Callback for when the dropdown open state changes.
    /// </summary>
    /// <remarks>
    /// Allows parent components to respond to changes in the dropdown's visibility.
    /// </remarks>
    /// <accessibility>
    /// Helps manage focus and aria-expanded states for dropdown menus.
    /// </accessibility>
    /// <example>
    /// <code>
    /// &lt;NavbarLink 
    ///     HasDropdown="true"
    ///     IsDropdownOpen="@isServicesDropdownOpen"
    ///     IsDropdownOpenChanged="@HandleDropdownToggle"&gt;
    ///     Services
    ///     &lt;DropdownContent&gt;
    ///         &lt;DropdownItem&gt;Web Design&lt;/DropdownItem&gt;
    ///         &lt;DropdownItem&gt;Consulting&lt;/DropdownItem&gt;
    ///     &lt;/DropdownContent&gt;
    /// &lt;/NavbarLink&gt;
    /// 
    /// @code {
    ///     private bool isServicesDropdownOpen;
    ///     
    ///     private void HandleDropdownToggle(bool isOpen)
    ///     {
    ///         isServicesDropdownOpen = isOpen;
    ///     }
    /// }
    /// </code>
    /// </example>
    [Parameter]
    public EventCallback<bool> IsDropdownOpenChanged { get; set; }

    private async Task HandleClick(MouseEventArgs args)
    {
        if (!Disabled)
        {
            await OnClick.InvokeAsync(args);
        }
    }

    private async Task ToggleDropdown()
    {
        if (!Disabled)
        {
            IsDropdownOpen = !IsDropdownOpen;
            await IsDropdownOpenChanged.InvokeAsync(IsDropdownOpen);
        }
    }

    private string GetLinkClasses()
    {
        return MergeClasses(
            ElementClass.Empty()
                .Add("block py-2 pr-4 pl-3 md:p-0")
                .Add("inline-flex items-center", when: HasDropdown)
                .Add("text-gray-700 hover:bg-gray-100 md:hover:bg-transparent md:border-0 md:hover:text-blue-700 dark:text-gray-400 md:dark:hover:text-white dark:hover:bg-gray-700 dark:hover:text-white md:dark:hover:bg-transparent", when: !Active && !Disabled)
                .Add("text-white bg-blue-700 md:bg-transparent md:text-blue-700 dark:text-white", when: Active)
                .Add("text-gray-400 hover:cursor-not-allowed", when: Disabled)
                .Add(Class)
        );
    }

    private string GetDropdownClasses()
    {
        return MergeClasses(
            ElementClass.Empty()
                .Add("absolute z-10 mt-2 w-48 rounded-md shadow-lg")
                .Add("bg-white dark:bg-gray-800 ring-1 ring-black ring-opacity-5")
                .Add("md:right-0 origin-top-right")
        );
    }
}
