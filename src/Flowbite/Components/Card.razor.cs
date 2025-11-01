using Microsoft.AspNetCore.Components;

namespace Flowbite.Components;

/// <summary>
/// Card component for displaying content in a box with various styles and layouts.
/// </summary>
public partial class Card
{
    private string BaseClasses => "flex rounded-lg border border-gray-200 bg-white shadow-md dark:border-gray-700 dark:bg-gray-800 transition-colors duration-200";

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

    /// <summary>
    /// Additional attributes to be applied to the card container.
    /// </summary>
    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object>? AdditionalAttributes { get; set; }

    private string? ComponentClasses
    {
        get
        {
            var classes = new List<string>
            {
                BaseClasses,
                Horizontal ? "flex-col md:flex-row" : "flex-col",
            };

            if (!string.IsNullOrEmpty(Href))
            {
                classes.Add("cursor-pointer hover:bg-gray-100 dark:hover:bg-gray-700");
            }

            return CombineClasses(string.Join(" ", classes.Where(c => !string.IsNullOrEmpty(c))));
        }
    }

    private string GetImageClasses() => Horizontal
        ? "h-96 w-full object-cover md:h-auto md:w-48 md:rounded-l-lg"
        : "w-full rounded-t-lg object-cover";
}
