using Microsoft.AspNetCore.Components;

namespace Flowbite.Components;

/// <summary>
/// Represents a collapsible section within a navigation bar, typically used for responsive mobile menus.
/// </summary>
/// <remarks>
/// Provides a container for navigation links and other content that can be toggled in mobile or responsive layouts.
/// </remarks>
public partial class NavbarCollapse
{
    /// <summary>
    /// Child content to be rendered inside the collapse section.
    /// </summary>
    /// <remarks>
    /// Allows flexible content rendering within the navbar's collapsible area, 
    /// such as navigation links, buttons, or other interactive elements.
    /// </remarks>
    /// <accessibility>
    /// Ensures that collapsed content is properly hidden and revealed for screen readers and keyboard navigation.
    /// </accessibility>
    /// <example>
    /// <code>
    /// &lt;NavbarCollapse&gt;
    ///     &lt;NavbarLink Href="/home"&gt;Home&lt;/NavbarLink&gt;
    ///     &lt;NavbarLink Href="/about"&gt;About&lt;/NavbarLink&gt;
    ///     &lt;NavbarLink Href="/contact"&gt;Contact&lt;/NavbarLink&gt;
    /// &lt;/NavbarCollapse&gt;
    /// 
    /// &lt;NavbarCollapse&gt;
    ///     &lt;div class="flex items-center"&gt;
    ///         &lt;Button Color="ButtonColor.Primary"&gt;Sign Up&lt;/Button&gt;
    ///         &lt;Button Color="ButtonColor.Secondary"&gt;Login&lt;/Button&gt;
    ///     &lt;/div&gt;
    /// &lt;/NavbarCollapse&gt;
    /// </code>
    /// </example>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }
}
