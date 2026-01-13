using Flowbite.Utilities;

namespace Flowbite.Components;

/// <summary>
/// Represents a logo component for the sidebar that can include an image and text.
/// </summary>
public partial class SidebarLogo
{
    /// <summary>
    /// Gets or sets the URL that the logo links to.
    /// </summary>
    [Parameter]
    public string Href { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the source URL of the logo image.
    /// </summary>
    [Parameter]
    public string ImgSrc { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the alt text for the logo image.
    /// </summary>
    [Parameter]
    public string ImgAlt { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the content to be displayed next to the logo image.
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    private string GetBaseClasses() => MergeClasses(
        ElementClass.Empty()
            .Add("flex items-center pl-1 mb-5")
            .Add(Class));

    private string GetImgClasses() => MergeClasses(
        ElementClass.Empty()
            .Add("h-6 mr-3 sm:h-7"));

    private string GetTextClasses() => MergeClasses(
        ElementClass.Empty()
            .Add("self-center text-xl font-semibold whitespace-nowrap dark:text-white"));
}
