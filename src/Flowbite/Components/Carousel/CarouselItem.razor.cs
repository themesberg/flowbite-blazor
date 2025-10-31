using Microsoft.AspNetCore.Components;
using Flowbite.Base;

namespace Flowbite.Components.Carousel;

/// <summary>
/// Represents an individual slide within a carousel component.
/// </summary>
public partial class CarouselItem : FlowbiteComponentBase, IDisposable
{
    private int _slideIndex = -1;
    private bool _isRegistered;

    /// <summary>
    /// Gets or sets the cascaded carousel instance.
    /// </summary>
    [CascadingParameter]
    private Carousel? ParentCarousel { get; set; }

    /// <summary>
    /// Gets or sets the cascaded carousel state.
    /// </summary>
    [CascadingParameter]
    private CarouselState? CarouselState { get; set; }

    /// <summary>
    /// Gets or sets the image source URL. If provided, an image will be rendered.
    /// </summary>
    [Parameter]
    public string? ImageSrc { get; set; }

    /// <summary>
    /// Gets or sets the alternative text for the image.
    /// </summary>
    [Parameter]
    public string ImageAlt { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the optional title text for the image.
    /// </summary>
    [Parameter]
    public string? ImageTitle { get; set; }

    /// <summary>
    /// Gets or sets how the image should fit within its container. Default is Cover.
    /// </summary>
    [Parameter]
    public CarouselImageFit ImageFit { get; set; } = CarouselImageFit.Cover;

    /// <summary>
    /// Gets or sets the custom content to be rendered in the slide.
    /// Only used if ImageSrc is not provided.
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// Gets or sets additional HTML attributes to be applied to the slide container.
    /// </summary>
    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object>? AdditionalAttributes { get; set; }

    /// <inheritdoc />
    protected override void OnInitialized()
    {
        base.OnInitialized();
        
        if (ParentCarousel != null && !_isRegistered)
        {
            ParentCarousel.RegisterSlide();
            _isRegistered = true;
        }
    }

    private string GetStyle()
    {
        bool isActive = CarouselState?.CurrentIndex == GetSlideIndex();
        
        string display = isActive ? "grid-area: 1/1;" : "grid-area: 1/1; visibility: hidden;";
        string transition = $"transition-opacity {ParentCarousel?.TransitionDuration ?? 1000}ms ease-in-out;";
        string opacity = isActive ? "opacity: 1;" : "opacity: 0;";
        
        return $"{display} {transition} {opacity}";
    }

    private string GetImageClass()
    {
        string fitClass = ImageFit switch
        {
            CarouselImageFit.Cover => "object-cover",
            CarouselImageFit.Contain => "object-contain",
            CarouselImageFit.Fill => "object-fill",
            CarouselImageFit.ScaleDown => "object-scale-down",
            CarouselImageFit.None => "object-none",
            _ => "object-cover"
        };

        return $"block w-full h-full {fitClass}";
    }

    private int GetSlideIndex()
    {
        // The slide index is determined by registration order
        // This is a simplified approach; in production, you might need a more robust system
        if (_slideIndex == -1)
        {
            _slideIndex = CarouselState?.CurrentIndex ?? 0;
        }
        return _slideIndex;
    }

    /// <inheritdoc />
    public void Dispose()
    {
        if (ParentCarousel != null && _isRegistered)
        {
            ParentCarousel.UnregisterSlide();
            _isRegistered = false;
        }
    }
}
