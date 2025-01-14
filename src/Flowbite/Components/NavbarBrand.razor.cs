using Microsoft.AspNetCore.Components;

namespace Flowbite.Components;

/// <summary>
/// Represents the brand section of a navigation bar, typically containing a logo or site name.
/// </summary>
/// <remarks>
/// Provides a flexible way to create a clickable brand element within a navbar, 
/// with support for custom content and navigation.
/// </remarks>
public partial class NavbarBrand
{
    /// <summary>
    /// The URL that the brand link points to.
    /// </summary>
    /// <remarks>
    /// When specified, the brand element becomes a clickable link that navigates to the given URL.
    /// If not provided, the brand element will be a static text or logo.
    /// </remarks>
    /// <accessibility>
    /// Ensures proper navigation and screen reader compatibility for the brand link.
    /// </accessibility>
    /// <example>
    /// <code>
    /// &lt;NavbarBrand Href="/"&gt;
    ///     &lt;img src="/logo.png" alt="Company Logo" /&gt;
    /// &lt;/NavbarBrand&gt;
    /// 
    /// &lt;NavbarBrand Href="/home"&gt;
    ///     My Website
    /// &lt;/NavbarBrand&gt;
    /// </code>
    /// </example>
    [Parameter]
    public string? Href { get; set; }

    /// <summary>
    /// Child content to be rendered inside the brand section.
    /// </summary>
    /// <remarks>
    /// Allows for flexible content rendering, such as logos, text, or complex HTML structures.
    /// </remarks>
    /// <example>
    /// <code>
    /// &lt;NavbarBrand&gt;
    ///     &lt;img src="/logo.png" alt="Company Logo" class="h-6" /&gt;
    ///     &lt;span class="ml-2 font-semibold"&gt;My Company&lt;/span&gt;
    /// &lt;/NavbarBrand&gt;
    /// </code>
    /// </example>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }
}
