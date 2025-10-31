namespace Flowbite.Components;

/// <summary>
/// Represents an image in a carousel with its source, alternative text, and optional title.
/// </summary>
public class CarouselImage
{
    /// <summary>
    /// Gets or sets the source URL of the image.
    /// </summary>
    public string Src { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the alternative text for the image for accessibility.
    /// </summary>
    public string Alt { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the optional title text displayed when hovering over the image.
    /// </summary>
    public string? Title { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="CarouselImage"/> class.
    /// </summary>
    public CarouselImage()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="CarouselImage"/> class with specified properties.
    /// </summary>
    /// <param name="src">The source URL of the image.</param>
    /// <param name="alt">The alternative text for the image.</param>
    /// <param name="title">The optional title text for the image.</param>
    public CarouselImage(string src, string alt, string? title = null)
    {
        Src = src;
        Alt = alt;
        Title = title;
    }
}
