using Microsoft.AspNetCore.Components;
using Flowbite.Base;
using Flowbite.Utilities;

namespace Flowbite.Components;

/// <summary>
/// A responsive navigation bar component with configurable options.
/// </summary>
/// <remarks>
/// Supports fluid width, rounded corners, border, and mobile menu toggling.
/// </remarks>
public partial class Navbar : FlowbiteComponentBase
{
    /// <summary>
    /// Determines if the navbar should use fluid container width, allowing it to stretch across the entire screen.
    /// </summary>
    /// <remarks>
    /// When set to true, the navbar will have full-width padding instead of being constrained to a fixed container.
    /// </remarks>
    /// <example>
    /// <code>
    /// &lt;Navbar Fluid="true"&gt;
    ///     &lt;!-- Navbar content --&gt;
    /// &lt;/Navbar&gt;
    /// </code>
    /// </example>
    [Parameter]
    public bool Fluid { get; set; }

    /// <summary>
    /// Determines if the navbar should have rounded corners.
    /// </summary>
    /// <remarks>
    /// Adds visual softness to the navbar's appearance by applying border-radius.
    /// </remarks>
    /// <example>
    /// <code>
    /// &lt;Navbar Rounded="true"&gt;
    ///     &lt;!-- Navbar with rounded corners --&gt;
    /// &lt;/Navbar&gt;
    /// </code>
    /// </example>
    [Parameter]
    public bool Rounded { get; set; }

    /// <summary>
    /// Determines if the navbar should have a border.
    /// </summary>
    /// <remarks>
    /// Adds a border around the navbar, which can help define its boundaries.
    /// </remarks>
    /// <example>
    /// <code>
    /// &lt;Navbar Border="true"&gt;
    ///     &lt;!-- Navbar with a border --&gt;
    /// &lt;/Navbar&gt;
    /// </code>
    /// </example>
    [Parameter]
    public bool Border { get; set; }

    /// <summary>
    /// Gets or sets whether the mobile menu is open.
    /// </summary>
    /// <remarks>
    /// Controls the visibility of the mobile navigation menu.
    /// Useful for responsive designs and mobile-friendly navigation.
    /// </remarks>
    /// <example>
    /// <code>
    /// &lt;Navbar MenuOpen="@isMobileMenuOpen"&gt;
    ///     &lt;!-- Navbar content --&gt;
    /// &lt;/Navbar&gt;
    /// </code>
    /// </example>
    [Parameter]
    public bool MenuOpen { get; set; }

    /// <summary>
    /// Callback event triggered when the mobile menu's open state changes.
    /// </summary>
    /// <remarks>
    /// Allows parent components to respond to changes in the mobile menu's visibility.
    /// Provides the new open state as a boolean value.
    /// </remarks>
    /// <accessibility>
    /// Useful for managing focus states and updating aria attributes for screen readers.
    /// </accessibility>
    /// <example>
    /// <code>
    /// &lt;Navbar MenuOpenChanged="@HandleMobileMenuToggle"&gt;
    ///     &lt;!-- Navbar content --&gt;
    /// &lt;/Navbar&gt;
    /// 
    /// @code {
    ///     private void HandleMobileMenuToggle(bool isOpen)
    ///     {
    ///         // Handle mobile menu state change
    ///     }
    /// }
    /// </code>
    /// </example>
    [Parameter]
    public EventCallback<bool> MenuOpenChanged { get; set; }

    /// <summary>
    /// Child content to be rendered inside the navbar.
    /// </summary>
    /// <remarks>
    /// Allows flexible composition of navbar content, such as brand logos, navigation links, and toggle buttons.
    /// Supports responsive design by allowing dynamic content rendering.
    /// </remarks>
    /// <accessibility>
    /// Ensure that navbar content is keyboard navigable and screen reader friendly.
    /// Use appropriate ARIA attributes for complex navigation structures.
    /// </accessibility>
    /// <example>
    /// <code>
    /// &lt;Navbar&gt;
    ///     &lt;NavbarBrand&gt;My App&lt;/NavbarBrand&gt;
    ///     &lt;NavbarToggle /&gt;
    ///     &lt;NavbarCollapse&gt;
    ///         &lt;NavbarLink&gt;Home&lt;/NavbarLink&gt;
    ///         &lt;NavbarLink&gt;About&lt;/NavbarLink&gt;
    ///     &lt;/NavbarCollapse&gt;
    /// &lt;/Navbar&gt;
    /// </code>
    /// </example>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    private string NavbarClasses => MergeClasses(
        ElementClass.Empty()
            .Add("bg-white dark:border-gray-700 dark:bg-gray-800 px-2 py-2.5 sm:px-4 w-full")
            .Add("rounded", when: Rounded)
            .Add("border", when: Border)
            .Add(Class)
    );

    private string ContainerClasses => MergeClasses(
        ElementClass.Empty()
            .Add("mx-auto", when: !Fluid)
            .Add("px-2 sm:px-4 py-2.5", when: Fluid)
            .Add("flex flex-wrap justify-between items-center")
    );

    protected override void OnInitialized()
    {
        // Subscribe to our own MenuOpenChanged event
        MenuOpenChanged = EventCallback.Factory.Create<bool>(this, OnMenuOpenChanged);
        base.OnInitialized();
    }

    private void OnMenuOpenChanged(bool isOpen)
    {
        MenuOpen = isOpen;
        StateHasChanged();
    }
}
