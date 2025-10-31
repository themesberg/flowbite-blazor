namespace Flowbite.Components;

/// <summary>
/// Defines how carousel images should be fitted within their container.
/// </summary>
public enum CarouselImageFit
{
    /// <summary>
    /// The image will cover the entire container, potentially cropping parts of the image.
    /// </summary>
    Cover,

    /// <summary>
    /// The image will be contained within the container, showing the entire image with possible letterboxing.
    /// </summary>
    Contain,

    /// <summary>
    /// The image will fill the container, potentially distorting the aspect ratio.
    /// </summary>
    Fill,

    /// <summary>
    /// The image will be scaled down to fit the container if larger, or displayed at its natural size if smaller.
    /// </summary>
    ScaleDown,

    /// <summary>
    /// The image will be displayed at its natural size without scaling.
    /// </summary>
    None
}

/// <summary>
/// Defines the position of carousel indicators.
/// </summary>
public enum CarouselIndicatorPosition
{
    /// <summary>
    /// Indicators will be positioned at the top of the carousel.
    /// </summary>
    Top,

    /// <summary>
    /// Indicators will be positioned at the bottom of the carousel.
    /// </summary>
    Bottom
}
