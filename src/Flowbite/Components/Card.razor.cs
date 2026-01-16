using Flowbite.Common;
using Flowbite.Utilities;
using Microsoft.AspNetCore.Components;

namespace Flowbite.Components;

/// <summary>
/// Card component for displaying content in a box with various styles and layouts.
/// </summary>
public partial class Card
{
    private const string BaseClasses = "flex rounded-lg border border-gray-200 bg-white shadow-md dark:border-gray-700 dark:bg-gray-800 transition-colors duration-200 motion-reduce:transition-none";
    private const string ImageBaseClasses = "object-cover";

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
    /// Slot configuration for per-element class customization.
    /// </summary>
    /// <remarks>
    /// Use slots to override default styling for specific parts of the card:
    /// - Base: The card container
    /// - Image: The card's image element
    /// - Body: The content wrapper (applied via ChildContent wrapper)
    /// </remarks>
    /// <example>
    /// <code>
    /// &lt;Card Slots="@(new CardSlots { Base = "shadow-xl", Image = "rounded-xl" })"&gt;
    ///     Content
    /// &lt;/Card&gt;
    /// </code>
    /// </example>
    [Parameter]
    public CardSlots? Slots { get; set; }

    private string ComponentClasses => MergeClasses(
        ElementClass.Empty()
            .Add(BaseClasses)
            .Add(Horizontal ? "flex-col md:flex-row" : "flex-col")
            .Add("cursor-pointer hover:bg-gray-100 dark:hover:bg-gray-700", when: !string.IsNullOrEmpty(Href))
            .Add(Slots?.Base)
            .Add(Class)
    );

    private string GetImageClasses() => MergeClasses(
        ElementClass.Empty()
            .Add(ImageBaseClasses)
            .Add(Horizontal ? "h-96 w-full md:h-auto md:w-48 md:rounded-l-lg" : "w-full rounded-t-lg")
            .Add(Slots?.Image)
    );

    private string GetBodyClasses() => MergeClasses(
        ElementClass.Empty()
            .Add(Slots?.Body)
    );

    private bool HasBodySlot => !string.IsNullOrWhiteSpace(Slots?.Body);
}
