using Flowbite.Utilities;
using Microsoft.AspNetCore.Components;

namespace Flowbite.Components;

/// <summary>
/// Card component for displaying content in a box with various styles and layouts.
/// </summary>
public partial class Card
{
    private const string BaseClasses = "flex rounded-lg border border-gray-200 bg-white shadow-md dark:border-gray-700 dark:bg-gray-800 transition-colors duration-200 motion-reduce:transition-none";

    /// <summary>
    /// Optional URL that the card will link to when clicked.
    /// </summary>
    [Parameter]
    public string? Href { get; set; }

    /// <summary>
    /// The source URL for the card's image.
    /// </summary>
    [Parameter]
    public string? ImgSrc { get; set; }

    /// <summary>
    /// The alt text for the card's image.
    /// </summary>
    [Parameter]
    public string? ImgAlt { get; set; }

    /// <summary>
    /// Whether the card should use a horizontal layout on larger screens.
    /// </summary>
    [Parameter]
    public bool Horizontal { get; set; }

    /// <summary>
    /// The content to be rendered inside the card.
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    private string ComponentClasses => MergeClasses(
        ElementClass.Empty()
            .Add(BaseClasses)
            .Add(Horizontal ? "flex-col md:flex-row" : "flex-col")
            .Add("cursor-pointer hover:bg-gray-100 dark:hover:bg-gray-700", when: !string.IsNullOrEmpty(Href))
            .Add(Class)
    );

    private string GetImageClasses() => Horizontal
        ? "h-96 w-full object-cover md:h-auto md:w-48 md:rounded-l-lg"
        : "w-full rounded-t-lg object-cover";
}
